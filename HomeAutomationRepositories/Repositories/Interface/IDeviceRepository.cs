using HomeAutomationRepositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Repositories.Interface
{
    public interface IDeviceRepository
    {
        Task<Device> GetDeviceAsync(string deviceId);
    }
}
