using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomationFrontEnd.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _service;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceService"></param>
        public DeviceController(IDeviceService deviceService)
        {
            _service = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{deviceId}")]
        public async Task<IActionResult> GetDeviceById(string deviceId)
        {
            return Ok(await _service.GetDeviceById(deviceId).ConfigureAwait(true));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateDevice(Device device)
        {
            return Ok(await _service.CreateDevice(device).ConfigureAwait(true));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="powerstrip"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Powerstrip")]
        public async Task<IActionResult> CreatePowerstrip(PowerStrip powerstrip)
        {
            return Ok(await _service.CreateDevice(powerstrip).ConfigureAwait(true));
        }
    }
}