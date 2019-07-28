using HomeAutomationRepositories.Entities;
using MongoDB.Driver;

namespace HomeAutomationRepositories.DataContext
{
    public interface IMongoContext<T> where T : class
    {
        IMongoCollection<T> MongoCollection { get; }
    }
}
