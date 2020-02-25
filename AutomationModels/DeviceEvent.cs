using BecauseImClever.HomeAutomation.DeviceBase.Abstractions;
using System;

namespace BecauseImClever.HomeAutomation.AutomationModels
{
    public class DeviceEvent : IDeviceEvent
    {
        public Guid CommandId => Guid.NewGuid();

        public DateTime Timestamp => DateTime.UtcNow;
    }
}
