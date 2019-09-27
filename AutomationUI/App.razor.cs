using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
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
                var bytes = await Http.GetByteArrayAsync("_framework/_bin/ClassLibrary1.dll");
                var assembly = System.Reflection.Assembly.Load(bytes);
                var t = assembly.GetType("ClassLibrary1.Class1");
                var m = t.GetMethod("GetMessage");
                Console.WriteLine($"ClassLibrary1.Class1.GetMessage(): {m.Invoke(null, null)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
