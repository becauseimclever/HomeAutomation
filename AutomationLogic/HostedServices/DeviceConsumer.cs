using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.AutomationModels;
using BecauseImClever.HomeAutomation.DeviceBase;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.AutomationLogic.HostedServices
{
    public class DeviceConsumer : IConsumer<Device>
    {
        private readonly IDeviceService _deviceService;

        public DeviceConsumer(IDeviceService deviceService)
        {
            _deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
        }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public async Task Consume(ConsumeContext<Device> context)
        {
            await _deviceService.DoTheThing(context.Message.Name).ConfigureAwait(false);
            await context.RespondAsync<IDeviceDone>(new { Value = $"Recieved: {context.Message.Name}" }).ConfigureAwait(false);
            await Console.Out.WriteLineAsync(context.Message.ToString()).ConfigureAwait(false);
            await Console.Out.WriteLineAsync(context.Message.GetType().Name).ConfigureAwait(false);
        }
    }
}
