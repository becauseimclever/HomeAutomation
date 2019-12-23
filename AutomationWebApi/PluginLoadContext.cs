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
	using System;
	using System.Reflection;
	using System.Runtime.Loader;

	public class PluginLoadContext : AssemblyLoadContext
	{
		private AssemblyDependencyResolver _resolver;
		public PluginLoadContext(string pluginPath)
		{
			_resolver = new AssemblyDependencyResolver(pluginPath);
		}
		protected override Assembly Load(AssemblyName assemblyName)
		{
			string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);

#pragma warning disable CA1307 // Specify StringComparison
			if (assemblyPath != null && assemblyPath.Contains("\\Plugins\\"))
#pragma warning restore CA1307 // Specify StringComparison
			{
				return LoadFromAssemblyPath(assemblyPath);
			}
			else if (AssemblyLoadContext.Default.LoadFromAssemblyName(assemblyName) != null)
			{
				return null;
			}
			return null;
		}
		protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
		{
			string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
			if (libraryPath != null) { return LoadUnmanagedDllFromPath(libraryPath); }
			return IntPtr.Zero;
		}
	}
}
