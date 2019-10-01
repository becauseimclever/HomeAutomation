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
using BecauseImClever.AutomationModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationUI.Pages
{

	public class SpecificRoomBase : ComponentBase
	{
		[CascadingParameter]
		public List<Room> RoomList { get; set; }
		[Inject] HttpClient httpClient { get; set; }
		public Room room { get; set; }
		[Parameter]
		public Guid id { get; set; }
		protected override void OnInitialized()
		{
			Console.WriteLine(id.ToString());
			LoadRoom();
		}
		protected void ValidSubmit()
		{

		}
		private void LoadRoom()
		{
			foreach (var r in RoomList)
			{
				Console.WriteLine(r.Id);
			}
			room = RoomList?.First(x => x.Id == id) ?? new Room();
		}
	}
}
