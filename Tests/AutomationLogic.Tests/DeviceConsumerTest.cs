using BecauseImClever.HomeAutomation.AutomationLogic.HostedServices;
using BecauseImClever.HomeAutomation.AutomationModels;
using MassTransit.Testing;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AutomationLogic.Tests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1065:Do not raise exceptions in unexpected locations", Justification = "<Pending>")]
    public class TestMessage : IDeviceEvent
    {
        public Guid CommandId => Guid.NewGuid();

        public DateTime Timestamp => DateTime.UtcNow;
    }
    public class DeviceConsumerTest
    {
        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
        public async Task ShouldTestConsumer()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<DeviceConsumer>();
            await harness.Start().ConfigureAwait(false);
            try
            {
                await harness.InputQueueSendEndpoint.Send(new TestMessage()).ConfigureAwait(false);
                Assert.True(harness.Consumed.Select<IDeviceEvent>().Any());
                Assert.True(consumerHarness.Consumed.Select<IDeviceEvent>().Any());
            }
            finally
            {
                await harness.Stop().ConfigureAwait(false);
            }
        }
    }
}
