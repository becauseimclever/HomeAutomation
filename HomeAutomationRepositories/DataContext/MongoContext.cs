using System;
using System.Collections.Generic;
using System.Text;
using HomeAutomationRepositories.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HomeAutomationRepositories.DataContext
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly string _roomCollection;

        public MongoContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _mongoDatabase = client.GetDatabase(settings.Value.Database);
            _roomCollection = settings.Value.RoomCollection;
        }
        public IMongoCollection<RoomEntity> RoomCollection => _mongoDatabase.GetCollection<RoomEntity>(_roomCollection);
    }
}
