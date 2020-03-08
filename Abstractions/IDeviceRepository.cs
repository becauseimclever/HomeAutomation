using BecauseImClever.HomeAutomation.DeviceBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.Abstractions
{
    public interface IDeviceRepository
    {
        ValueTask<Device> CreateAsync(Device device);
        ValueTask<IEnumerable<Device>> GetAllAsync();
        ValueTask<Device> GetByIdAsync(Guid Id);
        ValueTask<bool> UpdateAsync(Device device);
        ValueTask<bool> DeleteAsync(Guid Id);
    }
}
