using BecauseImClever.HomeAutomationRepositories.Entities;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomationRepositories.Repositories.Interfaces
{
    public interface IDeviceRepository
    {
        Task<Device> CreateDeviceAsync(Device device);
        Task<Device> GetDeviceAsync(string deviceId);
    }
}
