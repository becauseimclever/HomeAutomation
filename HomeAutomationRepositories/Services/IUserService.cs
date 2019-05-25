using HomeAutomationRepositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Services
{
    public interface IUserService
    {
        Task<UserEntity> Authenticate(string username, string password);
        Task<IEnumerable<UserEntity>> GetAll();
    }
}
