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
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        }
        public async ValueTask<Room> CreateAsync(Room room)
        {
            return await _roomRepository.CreateRoomAsync(room);
        }
        public async ValueTask<IEnumerable<Room>> GetAllAsync()
        {
            return await _roomRepository.GetAllAsync();
        }
        public async ValueTask<Room> GetByIdAsync(string Id)
        {
            return await _roomRepository.GetByIdAsync(Guid.Parse(Id));
        }
        public async ValueTask<bool> UpdateAsync(Room room)
        {
            return await _roomRepository.UpdateAsync(room);
        }
        public async ValueTask<bool> DeleteAsync(string Id)
        {
            return await _roomRepository.DeleteAsync(Id);
        }
    }
}
