using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeAutomationRepositories.DataContext;
using HomeAutomationRepositories.Entities;
using MongoDB.Driver;

namespace HomeAutomationRepositories.Repositories
{
    public class UserClaimRepository : IUserClaimRepository
    {
        public IMongoCollection<UserClaimEntity> userClaimCollection;
        public UserClaimRepository(IMongoContext context)
        {
            userClaimCollection = context.UserClaimCollection ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<UserClaimEntity> Create(UserClaimEntity claimEntity)
        {
            await userClaimCollection.InsertOneAsync(claimEntity);
            return claimEntity;
        }

        public Task<bool> Delete(string macAddress)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserClaimEntity>> GetAll()
        {
            return await userClaimCollection.Find(_ => true).ToListAsync();
        }

        public Task<UserClaimEntity> GetByMacAddress(string macAddress)
        {
            throw new NotImplementedException();
        }

        public Task<UserClaimEntity> Update(UserClaimEntity claimEntity)
        {
            throw new NotImplementedException();
        }
    }
}
