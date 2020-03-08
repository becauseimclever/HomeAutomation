using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.DeviceBase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.AutomationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
        }
        [HttpPost]
        [Route("")]
        public async ValueTask<IActionResult> CreateAsync([Required]Device device)
        {
            var newDevice = await _deviceService.CreateAsync(device);
            return Ok(newDevice);
        }
    }
}