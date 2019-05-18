using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Repositories
{
    public interface IRoomRepository
    {
        Task<List<RoomEntity>> GetAllAsync();
        Task<RoomEntity> GetByIdAsync(ObjectId Id);
        Task<RoomEntity> CreateRoomAsync(RoomEntity roomEntity);
        Task<bool> UpdateNameAsync(RoomEntity roomEntity);
        Task<bool> DeleteAsync(string id);
    }
}
