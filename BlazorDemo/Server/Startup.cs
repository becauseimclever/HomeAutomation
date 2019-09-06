using BecauseImClever.AutomationLogic.Interfaces;
using BecauseImClever.AutomationLogic.Services;
using BecauseImClever.AutomationRepositories;
using BecauseImClever.AutomationRepositories.DataContext;
using BecauseImClever.AutomationRepositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace BlazorDemo.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterRepositories(services);
            RegisterServices(services);
            RegisterOptions(services);

            services.AddMvc().AddNewtonsoftJson();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<Client.Startup>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
            });
        }
        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient(typeof(IMongoContext<>), typeof(MongoContext<>));
        }
        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IRoomRepository, RoomRepository>();
        }
        private void RegisterOptions(IServiceCollection services)
        {
            services.Configure<MongoSettings>(options =>
            {
                options.Database = Configuration.GetSection("MongoSettings:DataBase").Value;
                options.ConnectionString = Configuration.GetConnectionString("AutomationDb");
            });
        }

    }
}
