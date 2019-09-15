using System;
using System.Collections.Generic;

namespace BecauseImClever.DeviceBase
{
    public abstract class Device
    {
        public Guid Id { get; set; }
        public IEnumerable<IDeviceAction> Actions { get; internal set; }
    }
}
