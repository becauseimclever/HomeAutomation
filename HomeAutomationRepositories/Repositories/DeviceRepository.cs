using HomeAutomationRepositories.DataContext;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IMongoCollection<Device> _collection;

        public DeviceRepository(IMongoContext<Device> context)
        {
            var _context = context ?? throw new ArgumentNullException(nameof(context));
            _collection = _context.MongoCollection;
        }
        public async Task<Device> GetDeviceAsync(string deviceId)
        {
            var filter = Builders<Device>.Filter.Eq(x => x.Id, ObjectId.Parse(deviceId));
            var results = await _collection.FindAsync<Device>(filter).ConfigureAwait(true);
            return results.FirstOrDefault();
        }
    }
}
