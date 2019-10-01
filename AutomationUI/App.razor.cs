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
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationUI
{
    public class AppBase : ComponentBase
    {
        [Inject] HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadPlugins();
        }
        public async Task LoadPlugins()
        {
            try
            {
                var bytes = await Http.GetByteArrayAsync(@"api/Plugin/PowerStripPlugin");
                
                var assembly = System.Reflection.Assembly.Load(bytes);
                var t = assembly.GetType("BecauseImClever.PowerStripPlugin.PowerStrip");
                var m = t.GetMethod("RegisterDependencies");
                Console.WriteLine($"BecauseImClever.PowerStripPlugin.PowerStrip.RegisterDependencies(): {m.Invoke(t.TypeInitializer.Invoke(null) , null)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
