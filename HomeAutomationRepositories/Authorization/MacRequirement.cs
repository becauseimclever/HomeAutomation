using Microsoft.AspNetCore.Authorization;

namespace HomeAutomationRepositories.Authorization
{
    public class MacRequirement : IAuthorizationRequirement
    {
        public string policyName { get; }

        public MacRequirement(string policyName)
        {
            this.policyName = policyName;
        }
    }
}