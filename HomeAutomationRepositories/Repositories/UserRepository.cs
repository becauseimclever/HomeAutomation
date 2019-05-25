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
    public class UserRepository : IUserRepository
    {
        IMongoCollection<UserEntity> userCollection;
        public UserRepository(IMongoContext context)
        {
            userCollection = context?.UserCollection ?? throw new ArgumentNullException(nameof(context));
        }
        #region Create
        public async Task<UserEntity> CreateUserAsync(UserEntity userEntity)
        {
            await userCollection.InsertOneAsync(userEntity);
            return userEntity;
        }

        #endregion
        #region Read  
        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await userCollection.Find(_ => true).ToListAsync();
        }

        public async Task<UserEntity> GetByIdAsync(ObjectId Id)
        {
            var builder = Builders<UserEntity>.Filter;
            var filter = builder.Eq(x => x.Id, Id);
            return await userCollection.Find(filter).FirstOrDefaultAsync();
        }

        #endregion
        #region Update
        #endregion
        #region Delete
        #endregion
    }
}
