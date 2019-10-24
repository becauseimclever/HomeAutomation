﻿//	HomeAutomation - Home Automation system in .NET Core and Blazor, focusing on rooms and devices.
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
using BecauseImClever.AutomationModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecauseImClever.Abstractions
{
	public interface IPluginRepository
	{
		ValueTask<Plugin> CreatePluginAsync(Plugin plugin);
		ValueTask<Plugin> GetPluginAsync(Guid Id);
		ValueTask<IEnumerable<Plugin>> GetPluginsAsync();
		ValueTask<Plugin> UpdatePluginAsync(Plugin plugin);
		ValueTask<bool> DetelePluginAsync(Guid Id);
	}
}