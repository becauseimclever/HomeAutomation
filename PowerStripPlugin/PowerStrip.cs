using System;
using System.Collections.Generic;
using BecauseImClever.DeviceBase;
using Microsoft.Extensions.DependencyInjection;


namespace BecauseImClever.PowerStripPlugin
{
	public class PowerStrip : IDevicePlugin
	{

		public string Name => "PowerStrip";

		public string Description => "Represents the Powerstrip Plugin";

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1065:Do not raise exceptions in unexpected locations", Justification = "<Pending>")]
		public IEnumerable<IDeviceAction> Actions => new List<IDeviceAction>
		{

		};

		public void RegisterDependencies(IServiceCollection services)
		{
#pragma warning disable CA1303 // Do not pass literals as localized parameters
			Console.WriteLine("PowerStrip Register Dependencies");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
		}
	}
}
