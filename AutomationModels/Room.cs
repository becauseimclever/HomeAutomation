using BecauseImClever.DeviceBase;
using System;
using System.Collections.Generic;

namespace BecauseImClever.AutomationModels
{
    public class Room
    {
        public Room() { 
        
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Device> Devices { get; set; }
    }
}
