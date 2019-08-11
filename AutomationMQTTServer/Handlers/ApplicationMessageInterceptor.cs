using HomeAutomationRepositories.Services.Interface;
using MQTTnet;
using MQTTnet.Server;
using System;
using System.Threading.Tasks;

namespace AutomationMQTTServer.Handlers
{
    public class ApplicationMessageInterceptor : IMqttServerApplicationMessageInterceptor
    {
        private readonly IDeviceService _deviceService;
        public ApplicationMessageInterceptor(IDeviceService deviceService)
        {
            _deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
        }
        public async Task InterceptApplicationMessagePublishAsync(MqttApplicationMessageInterceptorContext context)
        {
            var _context = context ?? throw new ArgumentNullException(nameof(context));
            var objectId = context.ClientId.Substring(0, 24);
            var device = await _deviceService.GetDeviceById(objectId).ConfigureAwait(true);

            var topic = _context.ApplicationMessage.Topic;
            var segments = topic.Split('/');

            Console.WriteLine(device);
            Console.WriteLine(device.GetType());
            Console.WriteLine(_context.ClientId);
            Console.WriteLine(_context.ApplicationMessage.Topic);

            Console.WriteLine(_context.ApplicationMessage.ConvertPayloadToString());
            return;
        }


    }
}
