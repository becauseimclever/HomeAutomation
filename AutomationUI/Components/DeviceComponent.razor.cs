using BecauseImClever.DeviceBase;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationUI.Components
{
    public class DeviceComponentBase : ComponentBase
    {
        [Inject] HttpClient HttpClient { get; set; }
        [Parameter] public Device device { get; set; }

        protected async override Task OnInitializedAsync()
        {


            await base.OnInitializedAsync();
        }
    }
}
