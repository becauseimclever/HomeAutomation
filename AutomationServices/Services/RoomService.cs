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

        }

        public ValueTask<Room> CreateAsync(Room room)
        {
            throw new NotImplementedException();
        }
        public ValueTask<IEnumerable<Room>> GetAllAsync()
        {
            throw new NotImplementedException();
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
