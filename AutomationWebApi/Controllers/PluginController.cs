//	HomeAutomation - Home Automation system in .NET Core and Blazor, focusing on rooms and devices.
//	Copyright(C) 2019 Darren Swan
//	This program is free software: you can redistribute it and/or modify
//	it under the terms of the GNU General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.
//	This program is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//	GNU General Public License for more details.
//	You should have received a copy of the GNU General Public License
//	along with this program.If not, see<https://www.gnu.org/licenses/>.

namespace BecauseImClever.HomeAutomation.AutomationWebApi.Controllers
{
    using Abstractions;
    using BecauseImClever.HomeAutomation.AutomationModels;
    using BecauseImClever.HomeAutomation.DeviceBase.Abstractions;
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class PluginController : ControllerBase
    {
        private readonly IPluginService _pluginService;
        private readonly IPublishEndpoint _publishEndpoint;

        public PluginController(IPluginService pluginService, IPublishEndpoint publishEndpoint)
        {
            _pluginService = pluginService ?? throw new ArgumentNullException(nameof(pluginService));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }
        [HttpGet]
        [Route("")]
        public async ValueTask<IActionResult> GetAsync()
        {
            return Ok(await _pluginService.GetAllAsync().ConfigureAwait(false));
        }
        [HttpPost]
        [Route("")]
        public async ValueTask<IActionResult> PostAsync()
        {
            await _publishEndpoint.Publish<IDeviceEvent>(new DeviceEvent()).ConfigureAwait(false);
            return Ok("Success");
        }
    }
}