using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BecauseImClever.DeviceBase
{
    public interface IBooleanAction
    {
        ValueTask<bool> CurrentState(Guid Id);
        ValueTask<bool> SetState(Guid Id, bool state);
    }
}
