using HomeAutomationRepositories.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Diagnostics.CodeAnalysis;

namespace HomeAutomationRepositories.DataContext
{
    [ExcludeFromCodeCoverage]
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly string _roomCollection;

        public MongoContext(IOptions<MongoSettings> settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            var client = new MongoClient(settings.Value.ConnectionString);
            _mongoDatabase = client.GetDatabase(settings.Value.Database);
            _roomCollection = settings.Value.RoomCollection;
        }
        public IMongoCollection<Room> RoomCollection => _mongoDatabase.GetCollection<Room>(_roomCollection);
    }
}
