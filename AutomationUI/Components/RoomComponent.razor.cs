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
using BecauseImClever.DeviceBase;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationUI.Components
{
	public class RoomComponentBase : ComponentBase
	{
		[Inject] HttpClient httpClient { get; set; }
		[Parameter] public Room room { get; set; }
		[Parameter] public EventCallback Delete { get; set; }

		public async Task UpdateRoom()
		{
			var updatedRoom = await httpClient.PutAsync(@"api/room",
				new StringContent(JsonConvert.SerializeObject(room,
				new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }),
				Encoding.UTF8, "application/Json"));
			var newRoom = JsonConvert.DeserializeObject<Room>(await updatedRoom.Content.ReadAsStringAsync());
			Console.WriteLine(newRoom.Name + " was updated.");
		}
		public async Task DeleteRoom(Room room)
		{
			await httpClient.DeleteAsync($"api/room/{room.Id}");
			try
			{
				await Delete.InvokeAsync(room);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		public async Task NewDevice()
		{
			if (room.Devices == null)
				room.Devices = new List<Device>();

			var list = room.Devices.ToList();
			room.Devices = list;
			await UpdateRoom();
		}
	}
}
