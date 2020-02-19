using BecauseImClever.HomeAutomation.Abstractions;
using BecauseImClever.HomeAutomation.AutomationLogic.HostedServices;
using BecauseImClever.HomeAutomation.AutomationLogic.Services;
using BecauseImClever.HomeAutomation.AutomationRepositories;
using GreenPipes;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
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
            IBusControl CreateBus(IServiceProvider serviceProvider)
            {
                return Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://localhost:5672", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });
                    cfg.ReceiveEndpoint("device-action", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<DeviceConsumer>(serviceProvider);
                    });
                });
            }
            void ConfigureMassTransit(IServiceCollectionConfigurator configurator)
            {
                configurator.AddConsumer<DeviceConsumer>();
            }
            services.AddMassTransit(CreateBus, ConfigureMassTransit);
            return services;
        }


    }
}
