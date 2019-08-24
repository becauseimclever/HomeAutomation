using MongoDB.Driver;

namespace BecauseImClever.HomeAutomationRepositories.DataContext
{
    public interface IMongoContext<T> where T : class
    {
        IMongoCollection<T> MongoCollection { get; }
    }
}
