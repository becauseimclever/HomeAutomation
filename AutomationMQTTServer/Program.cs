using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MQTTnet.AspNetCore;
using System;

namespace AutomationMQTTServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel(x =>
            {
                x.ListenAnyIP(1883, y => y.UseMqtt());
            })
            .UseStartup<Startup>();
    }
}
