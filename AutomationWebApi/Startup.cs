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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomationWebApi;
using BecauseImClever.Abstractions;
using BecauseImClever.AutomationLogic.Services;
using BecauseImClever.AutomationRepositories;
using BecauseImClever.AutomationRepositories.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BecauseImClever.AutomationWebApi
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

			services.AddControllers().ConfigureApplicationPartManager(apm =>
			{
				var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(p => p.Assembly.FullName.Contains("Plugin", StringComparison.OrdinalIgnoreCase));
				foreach (var t in types)
				{
					apm.ApplicationParts.Add(new AssemblyPart(t.Assembly));
				}
			}).AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
				options.SerializerSettings.SerializationBinder = new DeviceDeserializer();
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Home Automation", Version = "v1" });
			});
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
				app.UseBlazorDebugging();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Home Automation V1");
			});
			app.UseStaticFiles();
			app.UseClientSideBlazorFiles<AutomationUI.Startup>();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
				endpoints.MapFallbackToClientSideBlazor<AutomationUI.Startup>("index.html");

			});

		}
		private static void RegisterServices(IServiceCollection services)
		{
			services.AddTransient<IRoomService, RoomService>();
			services.AddSingleton<IPluginService, PluginService>();
			services.AddTransient(typeof(IMongoContext<>), typeof(MongoContext<>));
			services.AddTransient<IMongoContext, MongoContext>();
		}
		private static void RegisterRepositories(IServiceCollection services)
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
