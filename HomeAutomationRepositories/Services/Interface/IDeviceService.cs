using HomeAutomationRepositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Services.Interface
{
    public interface IDeviceService
    {
        Task<Device> GetDeviceById(string deviceId);
        Task<Device> CreateDevice(Device device);
    }
}
