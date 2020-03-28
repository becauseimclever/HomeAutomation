using BecauseImClever.HomeAutomation.DeviceBase;
using BecauseImClever.HomeAutomation.DeviceBase.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.Abstractions
{
    public interface IDeviceRepository
    {
        ValueTask<IDevice> CreateAsync(IDevice device);
        ValueTask<IEnumerable<IDevice>> GetAllAsync();
        ValueTask<IDevice> GetByIdAsync(Guid Id);
        ValueTask<bool> UpdateAsync(IDevice device);
        ValueTask<bool> DeleteAsync(Guid Id);
    }
}
