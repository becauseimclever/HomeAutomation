using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.DeviceBase.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.AutomationRepositories
{
    public class DeviceRepository : IDeviceRepository
    {
        public ValueTask<IDevice> CreateAsync(IDevice device)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<IDevice>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask<IDevice> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> UpdateAsync(IDevice device)
        {
            throw new NotImplementedException();
        }
    }
}
