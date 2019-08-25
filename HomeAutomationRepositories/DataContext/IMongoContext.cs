using MongoDB.Driver;

namespace BecauseImClever.AutomationRepositories.DataContext
{
    public interface IMongoContext<T> where T : class
    {
        IMongoCollection<T> MongoCollection { get; }
    }
}
