using BecauseImClever.AutomationModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationUI.Components
{
    public class RoomComponentBase : ComponentBase
    {
        [Inject] HttpClient httpClient { get; set; }
        [Parameter] public Room room { get; set; }
        [Parameter] public EventCallback Delete { get; set; }
        public async Task UpdateRoom(Room room)
        {
            Console.WriteLine(room.Id);
            var updatedRoom = await httpClient.PutJsonAsync<Room>(@"api/room", room);
            Console.WriteLine(updatedRoom.Name + " was updated.");

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
    }
}
