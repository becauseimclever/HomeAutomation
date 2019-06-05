using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Services.Interface
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<User> CreateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByUserNameAsync(string userName);
    }
}
