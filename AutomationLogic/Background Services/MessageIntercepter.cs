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
            var applicationMessage = (context ?? throw new ArgumentNullException(nameof(context))).ApplicationMessage;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IDevicePlugin).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.FullName).ToList();

            foreach (var assembly in assemblies)
                Console.WriteLine(assembly);

            var splitTopic = applicationMessage.Topic.Split('/');
            var getTopLevel = splitTopic?[0];

            var service = Type.GetType(assemblies.FirstOrDefault(x => x.Contains(getTopLevel, StringComparison.InvariantCultureIgnoreCase)));
            if (service != null)
            {
                var newInstance = (IDevice)Activator.CreateInstance(service);
                newInstance.Invoke(applicationMessage.Topic, Encoding.UTF8.GetString(applicationMessage.Payload));

            }

#pragma warning disable CA1303 // Do not pass literals as localized parameters
            Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            Console.WriteLine($"+ Topic = {applicationMessage.Topic}");
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(applicationMessage.Payload)}");
            Console.WriteLine();

            return Task.CompletedTask;
        }
    }
}
