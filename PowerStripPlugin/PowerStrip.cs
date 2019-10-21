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
using System;
using System.Collections.Generic;
using BecauseImClever.DeviceBase;
using Microsoft.Extensions.DependencyInjection;
using PowerStripPlugin.Actions;

namespace BecauseImClever.PowerStripPlugin
{
    public class PowerStrip : IDevicePlugin
    {

        public string Name => "PowerStrip";

        public string Description => "Represents the Powerstrip Plugin";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1065:Do not raise exceptions in unexpected locations", Justification = "<Pending>")]
        public IEnumerable<IDeviceAction> Actions => new List<IDeviceAction>
        {
            new PowerStripActions(),
            new PowerStripActions(),
            new PowerStripActions(),
            new PowerStripActions(),
            new PowerStripActions(),
            new PowerStripActions(),
            new PowerStripActions(),
            new PowerStripActions()
        };

        public void RegisterDependencies(IServiceCollection services)
        {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            Console.WriteLine("PowerStrip Register Dependencies");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
        }
    }
}
