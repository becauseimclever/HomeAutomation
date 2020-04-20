using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.DeviceBase.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.AutomationLogic.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }
        public async ValueTask<IDevice> CreateAsync(IDevice device)
        {
            return await _deviceRepository.CreateAsync(device ?? throw new ArgumentNullException(nameof(device)));
        }

        public async ValueTask<bool> DeleteAsync(Guid Id)
        {
            return await _deviceRepository.DeleteAsync(Id).ConfigureAwait(false);
        }

        public async ValueTask<IEnumerable<IDevice>> GetAllAsync()
        {
            return await _deviceRepository.GetAllAsync().ConfigureAwait(false);
        }

        public async ValueTask<IDevice> GetByIdAsync(Guid Id)
        {
            return await _deviceRepository.GetByIdAsync(Id).ConfigureAwait(false);
        }

        public async ValueTask<bool> Update(IDevice device)
        {
            return await _deviceRepository.UpdateAsync(device).ConfigureAwait(false);
        }
    }
}
