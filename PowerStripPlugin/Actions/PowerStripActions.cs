using BecauseImClever.DeviceBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PowerStripPlugin.Actions
{
	public class PowerStripActions : IBooleanAction
	{
		public async ValueTask<bool> CurrentState(Guid Id)
		{
			return true;
		}

		public async ValueTask<bool> SetState(Guid Id, bool state)
		{
			throw new NotImplementedException();
		}
	}
}
