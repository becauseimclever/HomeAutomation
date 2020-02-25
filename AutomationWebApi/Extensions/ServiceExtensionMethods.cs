using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.AutomationLogic.HostedServices;
using BecauseImClever.HomeAutomation.AutomationLogic.Services;
using BecauseImClever.HomeAutomation.AutomationRepositories;
using BecauseImClever.HomeAutomation.DeviceBase;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        public static IServiceCollection AddMessageQueue(this IServiceCollection services)
        {
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<DeviceConsumer>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<DeviceConsumer>();
            });
            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.SetLoggerFactory(provider.GetService<ILoggerFactory>());
                cfg.ReceiveEndpoint("web-service-endpoint", e =>
                {
                    e.PrefetchCount = 16;
                    e.UseMessageRetry(x => x.Interval(2, 100));
                    e.Consumer<DeviceConsumer>(provider);
                    EndpointConvention.Map<Device>(e.InputAddress);
                });
            }));
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<Device>());
            services.AddSingleton<IHostedService, BusService>();


            return services;
        }


    }
}
