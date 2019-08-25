using BecauseImClever.AutomationModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationLogic.Interfaces
{
    public interface IRoomService
    {
        ValueTask<Room> UpdateAsync(Room room);
        ValueTask<bool> DeleteAsync(string id);

    }
}
