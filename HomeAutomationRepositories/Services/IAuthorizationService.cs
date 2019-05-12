using HomeAutomationRepositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Services
{
    public interface IAuthorizationService
    {
        Task<List<UserClaim>> GetAll();
        Task<UserClaim> GetByMacAddress(string macAddress);
        Task<UserClaim> Create(UserClaim userIdentity);
        Task<UserClaim> Update(UserClaim userIdentity);
        Task<bool> Delete(string macAddress);
    }
}
