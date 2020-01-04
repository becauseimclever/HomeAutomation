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


namespace BecauseImClever.HomeAutomation.AutomationUI.Pages
{
	using AutomationModels;
	using Microsoft.AspNetCore.Components;
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Threading.Tasks;
	public class RoomsBase : ComponentBase
	{
		[CascadingParameter]
		public List<Room> RoomList { get; set; }
		[Inject] HttpClient httpClient { get; set; }
		public bool isLoading = false;
		protected override async Task OnInitializedAsync()
		{
			await LoadRooms();
		}
		public async Task LoadRooms()
		{
			isLoading = true;
			if (!RoomList.Any())
			{
				var temp = await httpClient.GetAsync(@"api/room");
				Console.WriteLine(await temp.Content.ReadAsStringAsync());
				var temp2 = JsonConvert.DeserializeObject<List<Room>>(await temp.Content.ReadAsStringAsync(), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
				Console.WriteLine(temp2);
				RoomList = temp2;
				foreach (var room in RoomList)
				{
					Console.WriteLine(room.Id);
				}
			}
			isLoading = false;
		}
		public async Task CreateRoom()
		{
			var room = await httpClient.PostJsonAsync<Room>(@"api/room", new Room());
			RoomList.Add(room);
		}
	}
}
