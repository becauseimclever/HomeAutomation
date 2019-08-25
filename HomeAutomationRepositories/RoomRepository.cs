using BecauseImClever.AutomationModels;
using BecauseImClever.AutomationRepositories.DataContext;
using BecauseImClever.AutomationRepositories.Interfaces;
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
        }

        #region Create
        public async Task<Room> CreateRoomAsync(Room room)
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
                .Set(x => x.Name, roomEntity?.Name);
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
