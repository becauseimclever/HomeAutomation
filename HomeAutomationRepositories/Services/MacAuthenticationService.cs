using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeAutomationRepositories.Models;

namespace HomeAutomationRepositories.Services
{
    public class MacAuthenticationService : IMacAuthenticationService
    {
        public Task<UserClaim> GetUser(string macAddress)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsValidMacAsync(string mac)
        {
            throw new NotImplementedException();
        }
    }
}
