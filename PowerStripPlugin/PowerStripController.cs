using HomeAutomationRepositories.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace PowerStripPlugin
{
    [ApiController]
    [Route("api/[controller]")]
    public class PowerStripController : ControllerBase
    {
        private readonly IDeviceService _service;
        public PowerStripController(IDeviceService deviceService)
        {
            _service = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
        }
        [HttpGet]
        [Route("{deviceId}")]
        public async ValueTask<IActionResult> GetDeviceById(string deviceId)
        {
            return Ok(await _service.GetDeviceById(deviceId).ConfigureAwait(true));
        }

    }
}
