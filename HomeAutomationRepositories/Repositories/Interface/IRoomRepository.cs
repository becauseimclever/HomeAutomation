using BecauseImClever.HomeAutomationRepositories.Entities;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomationRepositories.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room> CreateRoomAsync(Room roomEntity);
        Task<List<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(ObjectId Id);
        Task<bool> UpdateAsync(Room roomEntity);
        Task<bool> DeleteAsync(string id);
    }
}
