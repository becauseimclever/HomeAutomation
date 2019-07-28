using HomeAutomationRepositories.Services.Interface;
using MQTTnet;
using MQTTnet.Server;
using System;
using System.Threading.Tasks;

namespace AutomationMQTTServer.Handlers
{
    public class ApplicationMessageInterceptor : IMqttServerApplicationMessageInterceptor
    {
        private readonly IRoomsService _roomService;
        public ApplicationMessageInterceptor(IRoomsService roomService)
        {
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
        }
        public async Task InterceptApplicationMessagePublishAsync(MqttApplicationMessageInterceptorContext context)
        {
            var _context = context ?? throw new ArgumentNullException(nameof(context));

            //var device = await _roomService.GetDeviceAsync(_context.ClientId).ConfigureAwait(true);



            var topic = _context.ApplicationMessage.Topic;
            var segments = topic.Split('/');

            foreach (var seg in segments)
            {
                Console.WriteLine(seg);
            }

            Console.WriteLine(_context.ClientId);
            Console.WriteLine(_context.ApplicationMessage.Topic);

            Console.WriteLine(_context.ApplicationMessage.ConvertPayloadToString());
            return;
        }
    }
}
