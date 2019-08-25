using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BecauseImClever.AutomationLogic.Interfaces;
using BecauseImClever.AutomationModels;
using BecauseImClever.AutomationRepositories.Interfaces;

namespace BecauseImClever.AutomationLogic.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async ValueTask<Room> CreateAsync(Room room)
        {
            return await _roomRepository.CreateRoomAsync(room);
        }
        public async ValueTask<IEnumerable<Room>> GetAllAsync()
        {
            return await _roomRepository.GetAllAsync();
        }
        public ValueTask<Room> GetByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }
        public ValueTask<Room> UpdateAsync(Room room)
        {
            throw new NotImplementedException();
        }
        public ValueTask<bool> DeleteAsync(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
