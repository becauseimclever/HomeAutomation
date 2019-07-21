using HomeAutomationRepositories.DataContext;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Repositories
{
    public class RoomRepository : IRoomRepository
    {

        public IMongoCollection<Room> roomCollection;

        public RoomRepository(IMongoContext context)
        {
            roomCollection = context?.RoomCollection ?? throw new ArgumentNullException(nameof(context));
        }

        #region Create
        public async Task<Room> CreateRoomAsync(Room room)
        {
            await roomCollection.InsertOneAsync(room);
            return room;
        }

        #endregion
        #region Read
        public async Task<List<Room>> GetAllAsync()
        {
            return await roomCollection.Find(_ => true).ToListAsync();
        }
        public async Task<Room> GetByIdAsync(ObjectId Id)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.Eq(x => x.Id, Id);

            return await roomCollection.Find(filter).FirstOrDefaultAsync();
        }
        #endregion
        #region Update

        public async Task<bool> UpdateAsync(Room roomEntity)
        {
            var builder = Builders<Room>.Filter;
            var filter = builder.Eq(x => x.Id, roomEntity.Id);

            var update = Builders<Room>.Update
                .Set(x => x.Name, roomEntity.Name)
                .Set(x => x.Devices, roomEntity.Devices);
            var updateOptions = new UpdateOptions() { IsUpsert = false };
            var returnValue = await roomCollection.UpdateOneAsync(filter, update, updateOptions);
            return returnValue.IsAcknowledged;

        }
        #endregion
        #region Delete
        public async Task<bool> DeleteAsync(string id)
        {
            var returnValue = await roomCollection.DeleteOneAsync(x => x.Id == ObjectId.Parse(id));
            return returnValue.IsAcknowledged;
        }
        #endregion
    }
}
