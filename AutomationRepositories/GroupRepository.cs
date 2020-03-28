//	HomeAutomation - Home Automation system in .NET Core and Blazor, focusing on rooms and devices.
//	Copyright(C) 2019 Darren Swan
//	This program is free software: you can redistribute it and/or modify
//	it under the terms of the GNU General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.
//	This program is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//	GNU General Public License for more details.
//	You should have received a copy of the GNU General Public License
//	along with this program.If not, see<https://www.gnu.org/licenses/>.


namespace BecauseImClever.HomeAutomation.AutomationRepositories
{
    using Abstractions;
    using AutomationModels;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class GroupRepository : IGroupRepository
    {

        private readonly IMongoCollection<Group> _groupCollection;

        public GroupRepository(IMongoDatabase mongoDatabase)
        {
            _groupCollection = (mongoDatabase ?? throw new ArgumentNullException(nameof(mongoDatabase))).GetCollection<Group>(typeof(Group).Name);
        }

        #region Create
        public async ValueTask<Group> CreateAsync(Group group)
        {
            var _group = group ?? throw new ArgumentNullException(nameof(group));
            await _groupCollection.InsertOneAsync(_group).ConfigureAwait(false);
            return _group;
        }
        public async ValueTask<IEnumerable<Group>> CreateManyAsync(IEnumerable<Group> groups)
        {
            await _groupCollection.InsertManyAsync(groups).ConfigureAwait(false);
            return groups;
        }
        #endregion
        #region Read
        public async ValueTask<List<Group>> GetAllAsync()
        {
            var filter = Builders<Group>.Filter.Empty;
            var results = await _groupCollection.FindAsync(filter).ConfigureAwait(true);

            return await results.ToListAsync().ConfigureAwait(true);
        }
        public async ValueTask<Group> GetByIdAsync(Guid Id)
        {
            var builder = Builders<Group>.Filter;
            var filter = builder.Eq(x => x.Id, Id);

            return await _groupCollection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(true);
        }

        #endregion
        #region Update

        public async ValueTask<bool> UpdateAsync(Group group)
        {
            var builder = Builders<Group>.Filter;
            var filter = builder.Eq(x => x.Id, group?.Id);

            var update = Builders<Group>.Update
                .Set(x => x.Name, group?.Name);
            var updateOptions = new UpdateOptions() { IsUpsert = false };
            var returnValue = await _groupCollection.UpdateOneAsync(filter, update, updateOptions).ConfigureAwait(true);
            return returnValue.IsAcknowledged;

        }

        #endregion
        #region Delete
        public async ValueTask<bool> DeleteAsync(Guid id)
        {
            var returnValue = await _groupCollection.DeleteOneAsync(x => x.Id == id).ConfigureAwait(true);

            return returnValue.IsAcknowledged;
        }
        public async ValueTask<(bool, long)> DeleteManyAsync(IEnumerable<Guid> ids)
        {
            var builder = Builders<Group>.Filter;
            var filter = builder.In(x => x.Id, ids);
            var returnValue = await _groupCollection.DeleteManyAsync(filter).ConfigureAwait(false);
            return (returnValue.IsAcknowledged, returnValue.DeletedCount);
        }
        #endregion
    }
}
