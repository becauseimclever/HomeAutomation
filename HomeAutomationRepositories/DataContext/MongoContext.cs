using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BecauseImClever.AutomationRepositories.DataContext
{
    [ExcludeFromCodeCoverage]

    public class MongoContext<T> : IMongoContext<T> where T : class
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoContext(IOptions<MongoSettings> settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            var client = new MongoClient(settings.Value.ConnectionString);
            _mongoDatabase = client.GetDatabase(settings.Value.Database);
        }
        public IMongoCollection<T> MongoCollection => _mongoDatabase.GetCollection<T>(typeof(T).Name);
        
    }
}
