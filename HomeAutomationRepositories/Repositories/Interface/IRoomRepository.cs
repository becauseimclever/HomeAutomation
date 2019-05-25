using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Repositories.Interface
{
    public interface IRoomRepository
    {
        Task<RoomEntity> CreateRoomAsync(RoomEntity roomEntity);
        Task<List<RoomEntity>> GetAllAsync();
        Task<RoomEntity> GetByIdAsync(ObjectId Id);
        Task<bool> UpdateAsync(RoomEntity roomEntity);
        Task<bool> DeleteAsync(string id);
    }
}
