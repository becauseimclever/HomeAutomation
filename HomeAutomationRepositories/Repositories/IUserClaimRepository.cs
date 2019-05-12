using HomeAutomationRepositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Repositories
{
    public interface IUserClaimRepository
    {
        Task<List<UserClaimEntity>> GetAll();
        Task<UserClaimEntity> GetByMacAddress(string macAddress);
        Task<UserClaimEntity> Create(UserClaimEntity claimEntity);
        Task<UserClaimEntity> Update(UserClaimEntity claimEntity);
        Task<bool> Delete(string macAddress);
    }
}
