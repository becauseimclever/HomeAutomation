using HomeAutomationRepositories.Plugins;
using HomeAutomationRepositories.Repositories;
using HomeAutomationRepositories.Repositories.Interfaces;
using HomeAutomationRepositories.Services;
using HomeAutomationRepositories.Services.Interface;
using Microsoft.Extensions.DependencyInjection;


namespace BecauseImClever.PowerStripPlugin
{
    public class PowerStrip : IDevicePlugin
    {
        public string Name => "PowerStrip";

        public string Description => "Represents the Powerstrip Plugin";

        public int Execute()
        {
            return 0;
        }

        public void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();
        }
    }
}
