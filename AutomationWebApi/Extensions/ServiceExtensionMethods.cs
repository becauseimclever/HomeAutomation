using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.AutomationLogic.BackgroundServices;
using BecauseImClever.HomeAutomation.AutomationLogic.Services;
using BecauseImClever.HomeAutomation.AutomationRepositories;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MQTTnet;
using MQTTnet.Adapter;
using MQTTnet.AspNetCore;
using MQTTnet.Implementations;
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
        public static IServiceCollection AddMQTTServer(this IServiceCollection services)
        {
            services.AddHostedMqttServer(builder =>
            {
                builder.WithDefaultEndpointPort(1883);
                builder.WithApplicationMessageInterceptor(new MessageIntercepter());
            });
            services.AddSingleton<MqttTcpServerAdapter>();
            services.AddSingleton<IMqttServerAdapter>(s => s.GetService<MqttTcpServerAdapter>());
            services.AddMqttWebSocketServerAdapter();
            services.AddSingleton<IMqttFactory, MqttFactory>();
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
