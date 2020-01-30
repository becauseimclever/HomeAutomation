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

namespace BecauseImClever.HomeAutomation.AutomationLogic.Services
{
    using Abstractions;
    using BecauseImClever.HomeAutomation.AutomationModels;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class PluginService : IPluginService
    {
        private readonly IPluginRepository _pluginRepository;
        public PluginService(IPluginRepository pluginRepository)
        {
            _pluginRepository = pluginRepository ?? throw new ArgumentNullException(nameof(pluginRepository));
        }
        public IEnumerable<string> GetAll()
        {
            var pluginFolder = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
            var pluginPaths = Directory.GetDirectories(pluginFolder);
            var dllPaths = pluginPaths.SelectMany(path =>
            {
                return Directory.GetFiles(path, "*Plugin.dll");
            });
            List<string> dllNames = new List<string>();
            foreach (var path in dllPaths)
            {
                dllNames.Add(Directory.GetParent(path).Name);
            }
            return dllNames;
        }


        public (Stream dll, string fileName) GetPlugin(string pluginName)
        {
            var pluginFolder = Path.Combine(Directory.GetCurrentDirectory(), "Plugins", pluginName);
            if (!Directory.Exists(pluginFolder)) return (null, null);
            var dllPaths = Directory.GetFiles(pluginFolder, "*Plugin.dll");
            if (!dllPaths.Any()) return (null, null);
            return (new FileStream(dllPaths.First(), FileMode.Open, FileAccess.Read), Path.GetFileName(dllPaths.First()));
        }
        public ValueTask<Plugin> CreateAsync(Plugin plugin)
        {
            return _pluginRepository.CreateAsync(plugin);
        }

        public ValueTask<IEnumerable<Plugin>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask<Plugin> GetAsync(Guid pluginId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> UpdatePluginAsync(Plugin plugin)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> DeleteAsync(Guid pluginId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<(bool, long)> DeleteManyAsync(IEnumerable<Guid> pluginIds)
        {
            throw new NotImplementedException();
        }
    }
}
