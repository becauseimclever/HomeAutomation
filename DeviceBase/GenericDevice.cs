using System;
using System.Collections.Generic;
using System.Text;

namespace BecauseImClever.DeviceBase
{
    public class GenericDevice : Device
    {
        public GenericDevice() { }
        public GenericDevice(string Name, Guid Id, IEnumerable<IDeviceAction> Actions) : base(Id, Actions)
        {
            this.Name = Name;
            this.Id = Id;
            this.Actions = Actions;
        }
        public string Name { get; set; }
    }
}
