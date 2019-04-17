using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Repositories
{
    public interface IRoomRepository
    {
        Task<List<RoomEntity>> GetAll();
        Task<RoomEntity> GetById(ObjectId Id);
        Task<RoomEntity> CreateRoom(RoomEntity roomEntity);
        Task<bool> Update(RoomEntity roomEntity);
        Task Delete(string id);
    }
}
