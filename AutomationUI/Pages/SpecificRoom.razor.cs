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
