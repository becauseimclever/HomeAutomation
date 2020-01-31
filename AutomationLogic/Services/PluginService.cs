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
    using System.Threading.Tasks;

    public class PluginService : IPluginService
    {
        private readonly IPluginRepository _pluginRepository;
        public PluginService(IPluginRepository pluginRepository)
        {
            _pluginRepository = pluginRepository ?? throw new ArgumentNullException(nameof(pluginRepository));
        }
        public ValueTask<Plugin> CreateAsync(Plugin plugin)
        {
            return _pluginRepository.CreateAsync(plugin);
        }

        public ValueTask<IEnumerable<Plugin>> GetAllAsync()
        {
            return _pluginRepository.GetAllAsync();
        }

        public ValueTask<Plugin> GetAsync(Guid pluginId)
        {
            return _pluginRepository.GetByIdAsync(pluginId);
        }

        public ValueTask<bool> UpdatePluginAsync(Plugin plugin)
        {
            return _pluginRepository.UpdateAsync(plugin);
        }

        public ValueTask<bool> DeleteAsync(Guid pluginId)
        {
            return _pluginRepository.DeleteAsync(pluginId);
        }

        public ValueTask<(bool, long)> DeleteManyAsync(IEnumerable<Guid> pluginIds)
        {
            return _pluginRepository.DeleteManyAsync(pluginIds);
        }
    }
}
