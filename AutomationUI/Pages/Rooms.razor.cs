using BecauseImClever.AutomationModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BecauseImClever.AutomationUI.Components;
using Newtonsoft.Json;
using BecauseImClever.DeviceBase;

namespace BecauseImClever.AutomationUI.Pages
{
    public class RoomsBase : ComponentBase
    {
        [Inject] HttpClient httpClient { get; set; }
        public List<Room> rooms { get; set; }
        public bool isLoading = false;
        protected override async Task OnInitializedAsync()
        {
            await LoadRooms();
        }
        public async Task LoadRooms()
        {
            isLoading = true;
            var temp = await httpClient.GetAsync(@"api/room");
            Console.WriteLine(await temp.Content.ReadAsStringAsync());
            var temp2 = JsonConvert.DeserializeObject<List<Room>>(await temp.Content.ReadAsStringAsync(), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            Console.WriteLine(temp2);
            rooms = temp2;

            isLoading = false;
        }
        public async Task CreateRoom()
        {
            var room = await httpClient.PostJsonAsync<Room>(@"api/room", new Room());
            rooms.Add(room);
        }
    }
}
