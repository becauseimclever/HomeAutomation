using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Authorization
{
    public class MacAuthorizationHandler : AuthorizationHandler<MacRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MacRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                return Task.CompletedTask;
            }
            var claimsRoles = context.User.FindAll(c => c.Type == ClaimTypes.Role);
            var requirementRoles = GetRequirementRoles(requirement);
            foreach (var req in requirementRoles)
            {
                foreach (var claim in claimsRoles)
                {
                    if (claim.Value == req)
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }

        private IEnumerable<string> GetRequirementRoles(MacRequirement requirement)
        {
            List<string> requirements = requirement.policyName.Split(',').ToList();
            requirements.Add(requirement.policyName);
            return requirements;
        }
    }
}
