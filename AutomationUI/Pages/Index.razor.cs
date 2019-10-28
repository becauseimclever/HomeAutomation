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
using System.Collections.Generic;
using System.Linq;

namespace BecauseImClever.AutomationUI.Pages
{
    public class IndexBase : ComponentBase
    {
        [CascadingParameter]
        protected List<Room> RoomList { get; set; }
        protected Room room { get; set; } = new Room() { Devices = new List<DeviceBase.Device>() };
        protected override void OnInitialized()
        {
            if (RoomList.Any())
                room = RoomList.FirstOrDefault();
        }
        protected void OnSelect(Room selected)
        {
            room = selected;
        }
    }
}
