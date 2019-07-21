using HomeAutomationRepositories.Entities;
using MongoDB.Driver;

namespace HomeAutomationRepositories.DataContext
{
    public interface IMongoContext
    {
        IMongoCollection<Room> RoomCollection { get; }
        IMongoCollection<UserEntity> UserCollection { get; }
    }
}
