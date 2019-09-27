using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace BecauseImClever.AutomationUI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
