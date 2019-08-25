using BecauseImClever.AutomationModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationLogic.Interfaces
{
    public interface IRoomService
    {
        ValueTask<Room> CreateAsync(Room room);
        ValueTask<IEnumerable<Room>> GetAllAsync();
        ValueTask<Room> GetByIdAsync(string Id);
        ValueTask<bool> UpdateAsync(Room room);
        ValueTask<bool> DeleteAsync(string id);

    }
}
