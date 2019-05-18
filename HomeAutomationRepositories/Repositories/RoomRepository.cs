using HomeAutomationRepositories.DataContext;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Repositories
{
    public class RoomRepository : IRoomRepository
    {

        public IMongoCollection<RoomEntity> roomCollection;

        public RoomRepository(IMongoContext context)
        {
            roomCollection = context.RoomCollection ?? throw new ArgumentNullException(nameof(context));
        }

        #region Create
        public async Task<RoomEntity> CreateRoomAsync(RoomEntity roomEntity)
        {
            await roomCollection.InsertOneAsync(roomEntity);
            return roomEntity;
        }

        #endregion
        #region Read
        public async Task<List<RoomEntity>> GetAllAsync()
        {
            return await roomCollection.Find(_ => true).ToListAsync();
        }
        public async Task<RoomEntity> GetByIdAsync(ObjectId Id)
        {
            var builder = Builders<RoomEntity>.Filter;
            var filter = builder.Eq(x => x.Id, Id);

            return await roomCollection.Find(filter).FirstOrDefaultAsync();
        }
        #endregion
        #region Update

        public async Task<bool> UpdateNameAsync(RoomEntity roomEntity)
        {
            var builder = Builders<RoomEntity>.Filter;
            var filter = builder.Eq(x => x.Id, roomEntity.Id);

            var update = Builders<RoomEntity>.Update.Set(x => x.Name, roomEntity.Name);

            var returnValue = await roomCollection.UpdateOneAsync(filter, update);
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
