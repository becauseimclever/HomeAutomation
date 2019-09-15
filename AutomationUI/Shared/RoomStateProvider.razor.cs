using BecauseImClever.AutomationModels;
using BecauseImClever.AutomationUI.Pages;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationUI.Shared
{
    public class RoomStateProviderBase : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        public List<Room> RoomList { get; set; }

    }
}
