using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.AutomationWebApi.Extensions
{
	public static class ServiceExtensionMethods
	{
		public static IServiceCollection AddKestrel(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<KestrelServerOptions>(configuration.GetSection("Kestrel"));

			return services;
		}
	}
}
