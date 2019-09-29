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