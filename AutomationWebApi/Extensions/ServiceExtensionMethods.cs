using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.AutomationLogic.Services;
using BecauseImClever.HomeAutomation.AutomationRepositories;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace BecauseImClever.HomeAutomation.AutomationWebApi.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensionMethods
    {
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public static IServiceCollection AddKestrel(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KestrelServerOptions>(configuration.GetSection("Kestrel"));

            return services;
        }
        public static IServiceCollection AddMongoDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(mc =>
            {
                var client = new MongoClient(configuration.GetConnectionString("AutomationDb"));
                return client.GetDatabase(configuration.GetSection("MongoSettings:Database").Value);
            });
            return services;
        }
        public static IServiceCollection AddAutomationServices(this IServiceCollection services)
        {
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddSingleton<IPluginService, PluginService>();
            return services;
        }
        public static IServiceCollection AddAutomationRespositories(this IServiceCollection services)
        {
            services.AddTransient<IDeviceRepository, DeviceRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IPluginRepository, PluginRepository>();
            return services;
        }
        public static IServiceCollection AddMessageQueue(this IServiceCollection services)
        {
            services.AddSingleton<IMessageService, MessageService>();

            return services;
        }


    }
}
