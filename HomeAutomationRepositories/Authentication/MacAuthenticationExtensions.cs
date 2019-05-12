using HomeAutomationRepositories.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Authentication
{
    public static class MacAuthenticationExtensions
    {
        public static AuthenticationBuilder AddMacAuth<TAuthService>(this AuthenticationBuilder builder)
            where TAuthService : class, IMacAuthenticationService
        {
            return AddMacAuth<TAuthService>(builder, MacAuthenticationDefaults.AuthenticationScheme, _ => { });
        }
        public static AuthenticationBuilder AddMacAuth<TAuthService>(this AuthenticationBuilder builder, string authenticationScheme)
            where TAuthService : class, IMacAuthenticationService
        {
            return AddMacAuth<TAuthService>(builder, authenticationScheme, _ => { });
        }
        public static AuthenticationBuilder AddMacAuth<TAuthService>(this AuthenticationBuilder builder, Action<MacAuthenticationOptions> configOptions)
            where TAuthService : class, IMacAuthenticationService
        {
            return AddMacAuth<TAuthService>(builder, MacAuthenticationDefaults.AuthenticationScheme, configOptions);
        }
        public static AuthenticationBuilder AddMacAuth<TAuthService>(this AuthenticationBuilder builder, string authenticationScheme, Action<MacAuthenticationOptions> configOptions)
            where TAuthService : class, IMacAuthenticationService
        {
            builder.Services.AddTransient<IMacAuthenticationService, TAuthService>();
            return builder.AddScheme<MacAuthenticationOptions, MacAuthenticationHandler>(authenticationScheme, configOptions);
        }
    }
}
