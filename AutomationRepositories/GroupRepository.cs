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
    public class GroupRepository : Repository<Group>, IGroupRepository
    {

        private readonly IMongoCollection<Group> _groupCollection;

        public GroupRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
            _groupCollection = (mongoDatabase ?? throw new ArgumentNullException(nameof(mongoDatabase))).GetCollection<Group>(typeof(Group).Name);
        }
        #region Update
        public async ValueTask<Group> UpdateAsync(Group group)
        {
            var builder = Builders<Group>.Filter;
            var filter = builder.Eq(x => x.Id, group?.Id);

            var update = Builders<Group>.Update
                .Set(x => x.Name, group?.Name);
            var updateOptions = new UpdateOptions() { IsUpsert = false };
            var returnValue = await _groupCollection.UpdateOneAsync(filter, update, updateOptions).ConfigureAwait(true);
            return returnValue.IsAcknowledged ? group : null;

        }
        public ValueTask<IEnumerable<Group>> UpdateManyAsync(IEnumerable<Group> groups)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
