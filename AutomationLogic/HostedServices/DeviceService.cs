using BecauseImClever.HomeAutomation.Abstractions;
using GreenPipes.Util;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.AutomationLogic.HostedServices
{
    public class DeviceService : IDeviceService
    {
        public Task DoTheThing(string value)
        {
            return TaskUtil.Completed;
        }
    }
}
