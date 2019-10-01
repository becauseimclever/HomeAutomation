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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BecauseImClever.AutomationLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PluginController : ControllerBase
    {
        private IPluginService _pluginService;
        public PluginController(IPluginService pluginService)
        {
            _pluginService = pluginService;
        }
        [HttpGet]
        [Route("")]
        public async ValueTask<IActionResult> GetAllAsync()
        {
            var plugins = await _pluginService.GetAll();
            return Ok(plugins);
        }
        [HttpGet]
        [Route("{PluginName}")]
        public async ValueTask<IActionResult> GetPluginAsync(string PluginName)
        {
            var plugin = await _pluginService.GetPluginAsync(PluginName);
            return File(plugin.dll, "application/octet-stream", plugin.fileName);
        }
    }
}