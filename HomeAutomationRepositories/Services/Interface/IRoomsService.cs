using BecauseImClever.HomeAutomationRepositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomationRepositories.Services.Interface
{
    public interface IRoomsService
    {
        Task<Room> CreateAsync(Room room);
        Task<List<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(string Id);
        Task<bool> UpdateAsync(Room room);
        Task<bool> DeleteAsync(string id);
    }
}
