using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomationRepositories.Models;
using HomeAutomationRepositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomationFrontEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll(string macAddress = null)
        {
            if (macAddress != null)
                return Ok(await _authorizationService.GetByMacAddress(macAddress));
            return Ok(await _authorizationService.GetAll());
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(UserClaim userClaim)
        {
            var newUser = userClaim ?? throw new ArgumentNullException(nameof(userClaim));
            return Ok(await _authorizationService.Create(newUser));
        }
    }
}