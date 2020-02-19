using System;

namespace BecauseImClever.HomeAutomation.AutomationModels
{
    public interface IDeviceEvent
    {
        Guid CommandId { get; }
        DateTime Timestamp { get; }

    }
}
