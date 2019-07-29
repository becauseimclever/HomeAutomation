using AutomationMQTTServer.Handlers;
using HomeAutomationRepositories.DataContext;
using HomeAutomationRepositories.Repositories;
using HomeAutomationRepositories.Repositories.Interfaces;
using HomeAutomationRepositories.Services;
using HomeAutomationRepositories.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Server;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace AutomationMQTTServer
{
    /// <summary>
    /// Main Program
    /// </summary>
    public static class Program
    {

        /// <summary>
        ///     The main method that starts the service.
        /// </summary>
        /// <param name="args">Some arguments. Currently unused.</param>
        public static async Task Main(string[] args)
        {

            var builder = new HostBuilder()
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.SetBasePath(Directory.GetCurrentDirectory());
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                        config.AddEnvironmentVariables();
                        if (args != null) { config.AddCommandLine(args); }
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddOptions();
                        services.Configure<MongoSettings>(options =>
                        {
                            options.Database = hostContext.Configuration.GetSection("MongoSettings:Database").Value;
                            options.ConnectionString = hostContext.Configuration.GetConnectionString("AutomationDb");

                        });
                        services.AddSingleton(typeof(IMongoContext<>), typeof(MongoContext<>));
                        services.AddSingleton<IHostedService, DaemonService>();
                        services.AddTransient<IRoomsService, RoomsService>();
                        services.AddTransient<IRoomRepository, RoomRepository>();
                        services.AddTransient<IMqttServerApplicationMessageInterceptor, ApplicationMessageInterceptor>();
                    })
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        logging.AddConsole();
                    });
            await builder.RunConsoleAsync().ConfigureAwait(false);
        }
    }
}
