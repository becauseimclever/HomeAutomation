﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Entities
{
	public class RoomEntity
	{
		public string Name { get; set; }
		public List<DeviceEntity> Devices { get; set; }
		
	}
}
