using HomeAutomationRepositories.Models;
using HomeAutomationRepositories.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Authentication
{
    public class MacAuthenticationHandler : AuthenticationHandler<MacAuthenticationOptions>
    {
        private readonly IMacAuthenticationService _authenticationService;
        public MacAuthenticationHandler(
            IOptionsMonitor<MacAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IMacAuthenticationService authenticationService) : base(options, logger, encoder, clock)
        {
            _authenticationService = authenticationService;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var macAddress = await GetMacAddress(Request.HttpContext.Connection.RemoteIpAddress.ToString());
            if (string.IsNullOrEmpty(macAddress) && Request.HttpContext.Connection.RemoteIpAddress.ToString() == "::1")
            {
                var identity = new UserClaim()
                {
                    FirstName = "Administrator",
                    LastName = "Administrator",
                    Groups = new List<string>() { "Admin" }
                };
                var claims = new List<Claim>();

                foreach (var group in identity.Groups) { claims.Add(new Claim(ClaimTypes.Role, group)); }

                var claimIdentity = new ClaimsIdentity(claims);
                var principal = new ClaimsPrincipal(claimIdentity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            if (!string.IsNullOrEmpty(macAddress) && await _authenticationService.IsValidMacAsync(macAddress))
            {
                var identity = await _authenticationService.GetUser(macAddress);
                var claims = new List<Claim>();

                foreach (var group in identity.Groups) { claims.Add(new Claim(ClaimTypes.Role, group)); }

                var claimIdentity = new ClaimsIdentity(claims);
                var principal = new ClaimsPrincipal(claimIdentity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            return AuthenticateResult.Fail("Not Authenticated");
        }
        private async Task<string> GetMacAddress(string ipAddress)
        {

            string macAddress = string.Empty;
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "arp";
            pProcess.StartInfo.Arguments = "-a " + ipAddress;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();
            string strOutput = pProcess.StandardOutput.ReadToEnd();
            string[] substrings = strOutput.Split('-');
            if (substrings.Length >= 8)
            {
                macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                    + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                    + "-" + substrings[7] + "-"
                    + substrings[8].Substring(0, 2);
                return macAddress;
            }
            else
            {
                return null;
            }
        }
    }
}
