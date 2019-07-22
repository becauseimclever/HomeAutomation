using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Server;

namespace AutomationMQTTServer.Handlers
{
    public class ApplicationMessageInterceptor : IMqttServerApplicationMessageInterceptor
    {
        public Task InterceptApplicationMessagePublishAsync(MqttApplicationMessageInterceptorContext context)
        {
            Console.WriteLine(context.ApplicationMessage.ConvertPayloadToString());
            return Task.CompletedTask;
        }
    }
}
