﻿using System;
using System.Reflection;
using System.Runtime.Loader;

namespace BecauseImClever.AutomationAPI

{
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

            if (assemblyPath != null && assemblyPath.Contains("\\Plugins\\"))
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
