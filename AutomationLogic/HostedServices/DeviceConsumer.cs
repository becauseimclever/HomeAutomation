using BecauseImClever.HomeAutomation.AutomationModels;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.AutomationLogic.HostedServices
{
    public class DeviceConsumer : IConsumer<IDeviceEvent>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public async Task Consume(ConsumeContext<IDeviceEvent> context)
        {
            await Console.Out.WriteLineAsync(context.Message.ToString()).ConfigureAwait(false);
            await Console.Out.WriteLineAsync(context.Message.GetType().Name).ConfigureAwait(false);
        }
    }
}
