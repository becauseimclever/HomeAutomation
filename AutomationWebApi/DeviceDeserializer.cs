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
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.RegularExpressions;
    [ExcludeFromCodeCoverage]
    public class DeviceDeserializer : DefaultSerializationBinder
    {
        private static readonly Regex regex = new Regex(
       @"System\.Private\.CoreLib(, Version=[\d\.]+)?(, Culture=[\w-]+)(, PublicKeyToken=[\w\d]+)?");
        private static readonly ConcurrentDictionary<Type, (string assembly, string type)> cache =
        new ConcurrentDictionary<Type, (string, string)>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            base.BindToName(serializedType, out assemblyName, out typeName);

            if (cache.TryGetValue(serializedType, out var name))
            {
                assemblyName = name.assembly;
                typeName = name.type;
            }
            else
            {
                if (assemblyName.AsSpan().Contains("System.Private.CoreLib".AsSpan(), StringComparison.OrdinalIgnoreCase))
                    assemblyName = regex.Replace(assemblyName, "mscorlib");

                if (typeName.AsSpan().Contains("System.Private.CoreLib".AsSpan(), StringComparison.OrdinalIgnoreCase))
                    typeName = regex.Replace(typeName, "mscorlib");

                cache.TryAdd(serializedType, (assemblyName, typeName));
            }
        }

    }
}
