using BecauseImClever.HomeAutomationRepositories.DataContext;
using BecauseImClever.HomeAutomationRepositories.Entities;
using BecauseImClever.HomeAutomationRepositories.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomationRepositories.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IMongoCollection<Device> _collection;

        public DeviceRepository(IMongoContext<Device> context)
        {
            var _context = context ?? throw new ArgumentNullException(nameof(context));
            _collection = _context.MongoCollection;
        }
        public async Task<Device> CreateDeviceAsync(Device device)
        {
            var _device = device ?? throw new ArgumentNullException(nameof(device));
            await _collection.InsertOneAsync(_device).ConfigureAwait(true);
            return _device;
        }
        public async Task<Device> GetDeviceAsync(string deviceId)
        {
            var filter = Builders<Device>.Filter.Eq(x => x.Id, ObjectId.Parse(deviceId));
            var results = await _collection.FindAsync<Device>(filter).ConfigureAwait(true);
            return results.FirstOrDefault();
        }
    }
}
