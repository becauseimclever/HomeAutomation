using HomeAutomationRepositories.Plugins;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HomeAutomationWithPlugins
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pluginFolder = Path.Combine(AppContext.BaseDirectory, "Plugins");
            if (!Directory.Exists(pluginFolder)) { Directory.CreateDirectory(pluginFolder); }
            var pluginPaths = Directory.GetDirectories(pluginFolder);
            var loaders = new List<PluginLoader>


            var host = CreateHostBuilder(args);
            foreach (IDevicePlugin plugin in devicePlugins)
            {
                Console.WriteLine($"{plugin.Name}\t - {plugin.Description}");
                host.ConfigureServices((hostContext, services) =>
                {
                    plugin.RegisterDependencies(services);
                });
            }
            host.Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
