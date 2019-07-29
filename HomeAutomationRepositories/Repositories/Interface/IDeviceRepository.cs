using HomeAutomationRepositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Repositories.Interfaces
{
    public interface IDeviceRepository
    {
        Task<Device> CreateDeviceAsync(Device device);
        Task<Device> GetDeviceAsync(string deviceId);
    }
}
