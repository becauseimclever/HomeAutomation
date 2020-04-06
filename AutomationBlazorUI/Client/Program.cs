using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.AutomationBlazorUI.Client
{
    public static class Program
    {
        public static async Task Main()
        {
            var builder = WebAssemblyHostBuilder.CreateDefault();
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
