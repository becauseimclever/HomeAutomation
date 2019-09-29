using System;
using System.Collections.Generic;

namespace BecauseImClever.DeviceBase
{
	public class Device
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<IDeviceAction> Actions { get; internal set; }
	}
}
