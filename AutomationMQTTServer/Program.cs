using AutomationMQTTServer.Handlers;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Server;
using System;

namespace AutomationMQTTServer
{
    /// <summary>
    /// Main Program
    /// </summary>
    public static class Program
    { /// <summary>
      ///     The main method that starts the service.
      /// </summary>
      /// <param name="args">Some arguments. Currently unused.</param>
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddTransient<IMqttServerApplicationMessageInterceptor, ApplicationMessageInterceptor>()
                .BuildServiceProvider();

            var optionsBuilder = new MqttServerOptionsBuilder()
                .WithDefaultEndpoint()
                .WithDefaultEndpointPort(1883)
                .WithApplicationMessageInterceptor(new ApplicationMessageInterceptor());


            var mqttServer = new MqttFactory().CreateMqttServer();
            mqttServer.StartAsync(optionsBuilder.Build());

            Console.ReadLine();
        }
    }
}
