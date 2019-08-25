using BecauseImClever.AutomationModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationRepositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room> CreateRoomAsync(Room roomEntity);
        Task<List<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(Guid Id);
        Task<bool> UpdateAsync(Room roomEntity);
        Task<bool> DeleteAsync(string id);
    }
}
