using HomeAutomationRepositories.Entities;
using MongoDB.Driver;

namespace HomeAutomationRepositories.DataContext
{
    public interface IMongoContext
    {
        IMongoCollection<RoomEntity> RoomCollection { get; }
        IMongoCollection<UserEntity> UserCollection { get; }
    }
}
