using BecauseImClever.DeviceBase;
using System;
using System.Collections.Generic;

namespace BecauseImClever.AutomationModels
{
	public class Room
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool Status { get; set; } = true;
		public IEnumerable<Device> Devices { get; set; }
	}
}
