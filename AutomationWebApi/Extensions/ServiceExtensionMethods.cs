using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.AutomationLogic.Services;
using BecauseImClever.HomeAutomation.AutomationRepositories;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MQTTnet;
using MQTTnet.Adapter;
using MQTTnet.AspNetCore;
using MQTTnet.Client;
using MQTTnet.Implementations;

namespace BecauseImClever.HomeAutomation.AutomationWebApi.Extensions
{
    public static class ServiceExtensionMethods
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
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
        public static IServiceCollection AddMQTTServer(this IServiceCollection services)
        {
            services.AddHostedMqttServer(builder => builder.WithDefaultEndpointPort(1883));
            services.AddMqttConnectionHandler();
            services.AddMqttWebSocketServerAdapter();
            services.AddScoped(sp => sp.GetRequiredService<IMqttFactory>().CreateMqttClient());
            return services;
        }
        public static IServiceCollection AddAutomationServices(this IServiceCollection services)
        {
            services.AddTransient<IRoomService, RoomService>();
            services.AddSingleton<IPluginService, PluginService>();
            return services;
        }
        public static IServiceCollection AddAutomationRespositories(this IServiceCollection services)
        {
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IPluginRepository, PluginRepository>();
            return services;
        }
    }
}
