using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Plugins
{
    public interface IDevicePlugin
    {
        string Name { get; }
        string Description { get; }
        int Execute();
        void RegisterDependencies(IServiceCollection services);
    }
}
