using BecauseImClever.HomeAutomation.DeviceBase.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BecauseImClever.HomeAutomation.AutomationModels
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<IDevice> Devices { get; internal set; }
        public void AddDevice(IDevice device)
        {
            Devices.ToList().Add(device);
        }
    }
}
