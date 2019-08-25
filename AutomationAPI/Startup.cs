using BecauseImClever.AutomationLogic.Interfaces;
using BecauseImClever.AutomationLogic.Services;
using BecauseImClever.AutomationRepositories;
using BecauseImClever.AutomationRepositories.DataContext;
using BecauseImClever.AutomationRepositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace BecauseImClever.AutomationAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterRepositories(services);
            RegisterServices(services);
            RegisterOptions(services);

            services.AddControllers().ConfigureApplicationPartManager(apm =>
            {
                var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.Assembly.FullName.Contains("Plugin", StringComparison.OrdinalIgnoreCase));
                foreach (var t in types)
                {
                    apm.ApplicationParts.Add(new AssemblyPart(t.Assembly));
                }
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Home Automation", Version = "v1" });
            });
            // In production, the Angular files will be served from this directory
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp/dist";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Home Automation V1");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            //app.UseSpa(spa =>
            //{
            //    // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //    // see https://go.microsoft.com/fwlink/?linkid=864501

            //    spa.Options.SourcePath = "ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});
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
