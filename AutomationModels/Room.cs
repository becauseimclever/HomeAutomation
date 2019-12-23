
//	HomeAutomation - Home Automation system in .NET Core and Blazor, focusing on rooms and devices.
//	Copyright(C) 2019 Darren Swan
//	This program is free software: you can redistribute it and/or modify
//	it under the terms of the GNU General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.
//	This program is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//	GNU General Public License for more details.
//	You should have received a copy of the GNU General Public License
//	along with this program.If not, see<https://www.gnu.org/licenses/>.
namespace BecauseImClever.HomeAutomation.AutomationModels
{

	using DeviceBase;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class Room
	{
		public Room() : this(Guid.NewGuid(), "New Room")
		{ }
		public Room(Guid Id) : this(Id, "New Room")
		{ }
		public Room(Guid Id, string Name)
		{
			this.Id = Id;
			this.Name = Name;
		}
		[Required]
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		public bool Status { get; set; } = true;
		public IEnumerable<Device> Devices { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime ModifiedDate { get; set; } = DateTime.Now;
	}
}
