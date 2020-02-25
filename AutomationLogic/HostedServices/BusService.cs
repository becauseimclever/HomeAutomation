using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.AutomationLogic.HostedServices
{
    public class BusService : IHostedService
    {
        private readonly IBusControl _busControl;
        public BusService(IBusControl busControl)
        {
            _busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _busControl.StopAsync(cancellationToken);
        }
    }
}
