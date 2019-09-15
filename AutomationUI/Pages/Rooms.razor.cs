using BecauseImClever.AutomationModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BecauseImClever.AutomationUI.Components;

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
            rooms = await httpClient.GetJsonAsync<List<Room>>(@"api/room");

            isLoading = false;
        }
        public async Task CreateRoom()
        {
            var room = await httpClient.PostJsonAsync<Room>(@"api/room", new Room());
            rooms.Add(room);
        }
    }
}
