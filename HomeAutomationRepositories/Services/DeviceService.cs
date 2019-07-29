using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Repositories.Interfaces;
using HomeAutomationRepositories.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }
        public async Task<Device> CreateDevice(Device device)
        {
            return await _deviceRepository.CreateDeviceAsync(device).ConfigureAwait(true);
        }

        public async Task<Device> GetDeviceById(string deviceId)
        {
            return await _deviceRepository.GetDeviceAsync(deviceId).ConfigureAwait(true);
        }
    }
}
