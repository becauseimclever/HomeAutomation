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
using BecauseImClever.AutomationModels;
using BecauseImClever.AutomationRepositories.DataContext;
using BecauseImClever.AutomationRepositories.Interfaces;
using BecauseImClever.DeviceBase;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationRepositories
{
    public class RoomRepository : IRoomRepository
    {

        private IMongoCollection<Room> _roomCollection;

        public RoomRepository(IMongoContext<Room> context)
        {
            var _context = context ?? throw new ArgumentNullException(nameof(context));
            _roomCollection = context.MongoCollection;
            if (!BsonClassMap.IsClassMapRegistered(typeof(GenericDevice))){ BsonClassMap.RegisterClassMap<GenericDevice>(); };
        }

        #region Create
        public async ValueTask<Room> CreateRoomAsync(Room room)
        {
            var _room = room ?? throw new ArgumentNullException(nameof(room));
            await _roomCollection.InsertOneAsync(_room).ConfigureAwait(true);
            return _room;
        }

        #endregion
        #region Read
        public async Task<List<Room>> GetAllAsync()
        {
            var filter = Builders<Room>.Filter.Empty;
            var results = await _roomCollection.FindAsync(filter).ConfigureAwait(true);

            return await results.ToListAsync().ConfigureAwait(true);
        }
        public async Task<Room> GetByIdAsync(Guid Id)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.Eq(x => x.Id, Id);

            return await _roomCollection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(true);
        }

        #endregion
        #region Update

        public async Task<bool> UpdateAsync(Room roomEntity)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.Eq(x => x.Id, roomEntity?.Id);

            var update = Builders<Room>.Update
                .Set(x => x.Name, roomEntity?.Name)
                .Set(x => x.Devices, roomEntity?.Devices);
            var updateOptions = new UpdateOptions() { IsUpsert = false };
            var returnValue = await _roomCollection.UpdateOneAsync(filter, update, updateOptions).ConfigureAwait(true);
            return returnValue.IsAcknowledged;

        }
        #endregion
        #region Delete
        public async Task<bool> DeleteAsync(string id)
        {
            var returnValue = await _roomCollection.DeleteOneAsync(x => x.Id == Guid.Parse(id)).ConfigureAwait(true);

            return returnValue.IsAcknowledged;
        }

        #endregion
    }
}
