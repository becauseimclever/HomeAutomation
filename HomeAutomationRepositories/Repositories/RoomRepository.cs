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

        public async Task<List<RoomEntity>> GetAll()
        {
            return await roomCollection.Find(_ => true).ToListAsync();
        }
        public async Task<RoomEntity> GetById(ObjectId Id)
        {
            var builder = Builders<RoomEntity>.Filter;
            var filter = builder.Eq(x => x.Id, Id);

            return await roomCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task CreateRoom(RoomEntity roomEntity)
        {
            await roomCollection.InsertOneAsync(roomEntity);
        }

        public async Task<bool> Update(RoomEntity roomEntity)
        {
            var builder = Builders<RoomEntity>.Filter;
            var filter = builder.Eq(x => x.Id, roomEntity.Id);

            var returnValue = await roomCollection.ReplaceOneAsync(filter, roomEntity);
            return returnValue.IsAcknowledged;

        }

        public async Task Delete(string id)
        {
            await roomCollection.DeleteOneAsync(x => x.Id == ObjectId.Parse(id));
        }
    }
}
