using HomeAutomationRepositories.Models;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Services
{
    public interface IMacAuthenticationService
    {
        Task<bool> IsValidMacAsync(string mac);
        Task<UserClaim> GetUser(string macAddress);
    }
}
