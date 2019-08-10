using HomeAutomationRepositories.DataContext;
using HomeAutomationRepositories.Repositories;
using HomeAutomationRepositories.Repositories.Interfaces;
using HomeAutomationRepositories.Services;
using HomeAutomationRepositories.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace AutomationFrontEnd
{
    /// <summary>
    /// Startup
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            MongoDefaults.GuidRepresentation = GuidRepresentation.Standard;


            RegisterServices(services);
            RegisterRepositories(services);
            RegisterOptions(services);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);



            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new Info { Title = "Home Automation", Version = "v1" });
               var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
               c.IncludeXmlComments(xmlPath);
           });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor
                                   | ForwardedHeaders.XForwardedProto
            });
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "Home Automation");
           });
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IRoomsService, RoomsService>();
            services.AddTransient<IDeviceService, DeviceService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddSingleton(typeof(IMongoContext<>), typeof(MongoContext<>));
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();

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
