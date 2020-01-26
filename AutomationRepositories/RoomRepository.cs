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
    public class RoomRepository : IRoomRepository
    {

        private IMongoCollection<Room> _roomCollection;

        public RoomRepository(IMongoDatabase mongoDatabase)
        {
            var _mongoDatabase = mongoDatabase ?? throw new ArgumentNullException(nameof(mongoDatabase));
            _roomCollection = mongoDatabase.GetCollection<Room>(typeof(Room).Name);
        }

        #region Create
        public async ValueTask<Room> CreateAsync(Room room)
        {
            var _room = room ?? throw new ArgumentNullException(nameof(room));
            await _roomCollection.InsertOneAsync(_room).ConfigureAwait(false);
            return _room;
        }
        public async ValueTask<IEnumerable<Room>> CreateManyAsync(IEnumerable<Room> rooms)
        {
            await _roomCollection.InsertManyAsync(rooms).ConfigureAwait(false);
            return rooms;
        }
        #endregion
        #region Read
        public async ValueTask<List<Room>> GetAllAsync()
        {
            var filter = Builders<Room>.Filter.Empty;
            var results = await _roomCollection.FindAsync(filter).ConfigureAwait(true);

            return await results.ToListAsync().ConfigureAwait(true);
        }
        public async ValueTask<Room> GetByIdAsync(Guid Id)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.Eq(x => x.Id, Id);

            return await _roomCollection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(true);
        }

        #endregion
        #region Update

        public async ValueTask<bool> UpdateAsync(Room room)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.Eq(x => x.Id, room?.Id);

            var update = Builders<Room>.Update
                .Set(x => x.Name, room?.Name)
                .Set(x => x.Devices, room?.Devices);
            var updateOptions = new UpdateOptions() { IsUpsert = false };
            var returnValue = await _roomCollection.UpdateOneAsync(filter, update, updateOptions).ConfigureAwait(true);
            return returnValue.IsAcknowledged;

        }

        #endregion
        #region Delete
        public async ValueTask<bool> DeleteAsync(Guid id)
        {
            var returnValue = await _roomCollection.DeleteOneAsync(x => x.Id == id).ConfigureAwait(true);

            return returnValue.IsAcknowledged;
        }
        public async ValueTask<(bool, long)> DeleteManyAsync(IEnumerable<Guid> ids)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.In(x => x.Id, ids);
            var returnValue = await _roomCollection.DeleteManyAsync(filter).ConfigureAwait(false);
            return (returnValue.IsAcknowledged, returnValue.DeletedCount);
        }
        #endregion
    }
}
