using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Repositories.Interface;
using HomeAutomationRepositories.Services.Interface;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly IRoomRepository _roomRepo;

        public RoomsService(IRoomRepository roomRepository)
        {
            _roomRepo = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        }

        public async Task<Room> CreateAsync(Room room)
        {
            var newRoom = await _roomRepo.CreateRoomAsync(room);

            return newRoom;
        }
        public async Task<List<Room>> GetAllAsync()
        {
            var rooms = await _roomRepo.GetAllAsync();

            return rooms;
        }

        public async Task<Room> GetByIdAsync(string Id)
        {
            var room = await _roomRepo.GetByIdAsync(ObjectId.Parse(Id));
            return room;
        }
        public async Task<bool> UpdateAsync(Room room)
        {
            return await _roomRepo.UpdateAsync(room);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _roomRepo.DeleteAsync(id);
        }

    }
}

