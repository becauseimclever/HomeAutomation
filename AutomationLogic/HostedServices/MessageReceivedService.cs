using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.AutomationLogic.HostedServices
{
    public class MessageReceivedService : BackgroundService
    {
        public IServiceProvider Services { get; }
        private readonly ConnectionFactory _factory;
        public MessageReceivedService(IServiceProvider services)
        {
            Services = services;
            _factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IConnection conn = _factory.CreateConnection();
            IModel channel = conn.CreateModel();
            channel.QueueDeclare(queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received from Rabbit: {0}", message);
            };
            channel.BasicConsume(queue: "hello",
                autoAck: true,
                consumer: consumer);
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
