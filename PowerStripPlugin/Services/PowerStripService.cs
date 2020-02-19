//	HomeAutomation - Home Automation system in .NET Core and Blazor, focusing on rooms and devices.
//	Copyright(C) 2019 Darren Swan
//	This program is free software: you can redistribute it and/or modify
//	it under the terms of the GNU General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.
//	This program is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//	GNU General Public License for more details.
//	You should have received a copy of the GNU General Public License
//	along with this program.If not, see<https://www.gnu.org/licenses/>.


namespace BecauseImClever.HomeAutomation.PowerStripPlugin.Services
{
    using Microsoft.Extensions.Hosting;
    using MQTTnet;
    using MQTTnet.Client;
    using MQTTnet.Client.Options;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    //Excluding until I know how I want to deal with this
    [ExcludeFromCodeCoverage]
    public class PowerStripService : BackgroundService
    {
        private readonly IMqttFactory _mqttFactory;
        public PowerStripService()
        {
            _mqttFactory = new MqttFactory();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            Console.WriteLine("MQTT POWERSTRIP");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            var mqttClient = _mqttFactory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                .WithClientId("PowerStripServer")
                .WithCleanSession()
                .WithTcpServer("localhost")
                .Build();

            mqttClient.UseDisconnectedHandler(async e =>
           {
               await mqttClient.ConnectAsync(options, CancellationToken.None).ConfigureAwait(false);
           });
            mqttClient.UseConnectedHandler(async e =>
            {
                await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("Outlet/0").Build()).ConfigureAwait(false);
            });
            mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
                Console.WriteLine();
            });
            while (true)
            {
                if (!mqttClient.IsConnected)
                    await mqttClient.ConnectAsync(options).ConfigureAwait(false);
            }
        }
    }
}
