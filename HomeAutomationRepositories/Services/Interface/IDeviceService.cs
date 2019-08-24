using BecauseImClever.HomeAutomationRepositories.Entities;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomationRepositories.Services.Interface
{
    public interface IDeviceService
    {
        Task<Device> GetDeviceById(string deviceId);
        Task<Device> CreateDevice(Device device);
    }
}
