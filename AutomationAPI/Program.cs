using BecauseImClever.HomeAutomationRepositories.Plugins;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BecauseImClever.AutomationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args);
            RegisterPlugins(host);
            host.Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                 });
        static void RegisterPlugins(IHostBuilder host)
        {
            var pluginFolder = Path.Combine(AppContext.BaseDirectory, "Plugins");
            if (!Directory.Exists(pluginFolder)) { Directory.CreateDirectory(pluginFolder); }
            var pluginPaths = Directory.GetDirectories(pluginFolder);
            var dllPaths = pluginPaths.SelectMany(path =>
            {
                return Directory.GetFiles(path, "*Plugin.dll");
            });

            IEnumerable<IDevicePlugin> devicePlugins = dllPaths.SelectMany(pluginPath =>
            {
                Assembly pluginAssembly = LoadPlugin(pluginPath);
                return CreateDevices(pluginAssembly);
            }).ToList();
            foreach (var pluginType in devicePlugins)
            {
                host.ConfigureServices((hostContext, services) => pluginType.RegisterDependencies(services));
            }
        }
        static Assembly LoadPlugin(string relativePath)
        {
            Console.WriteLine($"Loading device plugins from: {relativePath}");
            PluginLoadContext loadContext = new PluginLoadContext(relativePath);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(relativePath)));
        }
        static IEnumerable<IDevicePlugin> CreateDevices(Assembly assembly)
        {
            int count = 0;
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IDevicePlugin).IsAssignableFrom(type))
                {
                    IDevicePlugin result = Activator.CreateInstance(type) as IDevicePlugin;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }
            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                //    throw new ApplicationException(
                //$"Can't find any type which implements IDevicePlugin in {assembly} from {assembly.Location}.\n" +
                //$"Available types: {availableTypes}");
            }
        }
    }
}
