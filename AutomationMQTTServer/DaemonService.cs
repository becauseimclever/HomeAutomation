using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationMQTTServer.Handlers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Server;

namespace AutomationMQTTServer
{
    public sealed class DaemonService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IMqttServerApplicationMessageInterceptor _messageInterceptor;
        public DaemonService(ILogger<DaemonService> logger, IMqttServerApplicationMessageInterceptor messageInterceptor)
        {
            _logger = logger;
            _messageInterceptor = messageInterceptor;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var optionsBuilder = new MqttServerOptionsBuilder()
               .WithDefaultEndpoint()
               .WithDefaultEndpointPort(1883)
               .WithApplicationMessageInterceptor(_messageInterceptor);


            var mqttServer = new MqttFactory().CreateMqttServer();
            mqttServer.StartAsync(optionsBuilder.Build());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping daemon.");
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _logger.LogInformation("Disposing....");
        }
    }
}