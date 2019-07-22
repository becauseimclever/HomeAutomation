using MQTTnet;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutomationMQTTServer.Handlers
{
    public class RetainedMessageHandler : IMqttServerStorage
    {
        Task<IList<MqttApplicationMessage>> IMqttServerStorage.LoadRetainedMessagesAsync()
        {
            throw new NotImplementedException();
        }

        Task IMqttServerStorage.SaveRetainedMessagesAsync(IList<MqttApplicationMessage> messages)
        {
            throw new NotImplementedException();
        }
    }
}
