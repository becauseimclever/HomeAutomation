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
