using HomeAutomationRepositories.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Repositories.Interface

{
    public interface IUserRepository
    {
        Task<UserEntity> CreateUserAsync(UserEntity userEntity);
        Task<IEnumerable<UserEntity>> GetAllAsync();
        Task<UserEntity> GetByIdAsync(ObjectId Id);
        Task<UserEntity> GetByUserNameAsync(string userName);
        Task<UserEntity> AuthenticateUserAsync(string userName, string password);
    }
}
