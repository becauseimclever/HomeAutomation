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
	using Microsoft.AspNetCore.Mvc;

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
		public IActionResult GetAllAsync()
		{
			var plugins = _pluginService.GetAll();
			return Ok(plugins);
		}
		[HttpGet]
		[Route("{PluginName}")]
		public IActionResult GetPluginAsync(string PluginName)
		{
			var plugin = _pluginService.GetPlugin(PluginName);
			if (plugin.dll == null) return NotFound();
			return File(plugin.dll, "application/octet-stream", plugin.fileName);
		}
	}
}