using MQTTnet.Server;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using BecauseImClever.HomeAutomation.DeviceBase.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace BecauseImClever.HomeAutomation.AutomationLogic.BackgroundServices
{
    //Excluding until I figure out how I want to handle the message routing
    [ExcludeFromCodeCoverage]
    public class MessageIntercepter : IMqttServerApplicationMessageInterceptor
    {
        public Task InterceptApplicationMessagePublishAsync(MqttApplicationMessageInterceptorContext context)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IDevicePlugin).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();
            foreach (var assembly in assemblies)
            {
                Console.WriteLine(assembly);
            }
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
#pragma warning disable CA1062 // Validate arguments of public methods
            Console.WriteLine($"+ Topic = {context.ApplicationMessage.Topic}");
#pragma warning restore CA1062 // Validate arguments of public methods
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(context.ApplicationMessage.Payload)}");
            Console.WriteLine($"+ QoS = {context.ApplicationMessage.QualityOfServiceLevel}");
            Console.WriteLine($"+ Retain = {context.ApplicationMessage.Retain}");
            Console.WriteLine();

            return Task.CompletedTask;

        }
    }
}
