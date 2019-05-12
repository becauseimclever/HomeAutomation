using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Authorization
{
    public class MacAuthorizeAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "MAC";
        public MacAuthorizeAttribute(string group) => Group = group;
        public string Group
        {
            get { return Policy.Substring(POLICY_PREFIX.Length); }
            set
            {
                Policy = $"{POLICY_PREFIX}{value.ToString()}";
            }
        }
    }
}
