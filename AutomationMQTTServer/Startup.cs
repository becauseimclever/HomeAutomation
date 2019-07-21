using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MQTTnet.AspNetCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace AutomationMQTTServer
{
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

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            MongoDefaults.GuidRepresentation = GuidRepresentation.Standard;

            services.AddHostedMqttServer(options =>
            {
                options.WithDefaultEndpoint();
                options.Build();
            });
            services.AddMqttConnectionHandler();
            services.AddMqttWebSocketServerAdapter();

            RegisterServices(services);
            RegisterHandlers(services);
            RegisterRepositories(services);
            RegisterOptions(services);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

        }

        private void RegisterOptions(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        private void RegisterHandlers(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        private void RegisterServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}