using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.AutomationModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.AutomationRepositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _entityCollection;
        public Repository(IMongoDatabase mongoDatabase)
        {
            _entityCollection = (mongoDatabase ?? throw new ArgumentNullException(nameof(mongoDatabase))).GetCollection<T>(typeof(T).Name);
        }
        public async ValueTask<T> CreateAsync(T entity)
        {
            var _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            await _entityCollection.InsertOneAsync(_entity).ConfigureAwait(false);
            return _entity;
        }
        public async ValueTask<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities)
        {
            var _entities = entities ?? throw new ArgumentNullException(nameof(entities));
            await _entityCollection.InsertManyAsync(_entities).ConfigureAwait(false);
            return _entities;
        }
        public async ValueTask<IEnumerable<T>> GetAllAsync()
        {
            var filter = Builders<T>.Filter.Empty;
            var results = await _entityCollection.FindAsync(filter).ConfigureAwait(false);
            return await results.ToListAsync().ConfigureAwait(false);
        }
        public async ValueTask<T> GetByIdAsync(Guid id)
        {
            var builder = Builders<T>.Filter;
            var filter = builder.Eq(x => x.Id, id);
            return await _entityCollection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        public async ValueTask<bool> DeleteAsync(Guid id)
        {
            var returnValue = await _entityCollection.DeleteOneAsync(x => x.Id == id).ConfigureAwait(true);

            return returnValue.IsAcknowledged;
        }
        public async ValueTask<(bool, long)> DeleteManyAsync(IEnumerable<Guid> ids)
        {
            var builder = Builders<T>.Filter;
            var filter = builder.In(x => x.Id, ids);
            var returnValue = await _entityCollection.DeleteManyAsync(filter).ConfigureAwait(false);
            return (returnValue.IsAcknowledged, returnValue.DeletedCount);
        }
    }
}




