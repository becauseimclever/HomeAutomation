using BecauseImClever.HomeAutomation.DeviceBase.Abstractions;
using BecauseImClever.HomeAutomation.PowerStripPlugin.Actions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerStripPlugin.Models
{
    public class Powerstrip : IDevice
    {
       public Powerstrip()
        {
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<IDeviceAction> Actions => new List<PowerStripActions>();

        public ValueTask Invoke(string TopicPath, string Payload)
        {
            throw new NotImplementedException();
        }
    }
}
