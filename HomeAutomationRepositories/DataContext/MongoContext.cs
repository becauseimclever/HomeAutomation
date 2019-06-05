using HomeAutomationRepositories.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace HomeAutomationRepositories.DataContext
{
    [ExcludeFromCodeCoverage]
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly string _roomCollection;
        private readonly string _userClaimCollection;
        [ExcludeFromCodeCoverage]
        public MongoContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _mongoDatabase = client.GetDatabase(settings.Value.Database);
            _roomCollection = settings.Value.RoomCollection;
            _userClaimCollection = settings.Value.UserClaimCollection;
                    }
        [ExcludeFromCodeCoverage]
        public IMongoCollection<RoomEntity> RoomCollection => _mongoDatabase.GetCollection<RoomEntity>(_roomCollection);
        [ExcludeFromCodeCoverage]
        public IMongoCollection<UserEntity> UserCollection => _mongoDatabase.GetCollection<UserEntity>(_userClaimCollection);
    }
}
