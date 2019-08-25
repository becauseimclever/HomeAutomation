using BecauseImClever.DeviceBase;
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

        }
    }
}
