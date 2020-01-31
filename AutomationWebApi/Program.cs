//	HomeAutomation - Home Automation system in .NET Core and Blazor, focusing on rooms and devices.
//	Copyright(C) 2019 Darren Swan
//	This program is free software: you can redistribute it and/or modify
//	it under the terms of the GNU General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.
//	This program is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//	GNU General Public License for more details.
//	You should have received a copy of the GNU General Public License
//	along with this program.If not, see<https://www.gnu.org/licenses/>.


namespace BecauseImClever.HomeAutomation.AutomationWebApi
{
    using DeviceBase.Abstractions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main()
        {
            var host = CreateHostBuilder();
            RegisterPlugins(host);
            host.Build().Run();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            ;
        static void RegisterPlugins(IHostBuilder host)
        {
            var pluginFolder = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
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
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IDevicePlugin).IsAssignableFrom(type))
                {
                    IDevicePlugin result = Activator.CreateInstance(type) as IDevicePlugin;
                    if (result != null)
                    {
                        Console.WriteLine(type.Name);
                        yield return result;
                    }
                }
            }
        }


    }
}
