using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
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
            RoomEntity roomEntity = Room.ConvertToEntity(room);

            var newRoom = await _roomRepo.CreateRoomAsync(roomEntity);

            return RoomEntity.ConvertToModel(newRoom);
        }
        public async Task<List<Room>> GetAllAsync()
        {
            var rooms = await _roomRepo.GetAllAsync();
            var roomsList = new List<Room>();
            foreach (var room in rooms)
            {
                roomsList.Add(RoomEntity.ConvertToModel(room));
            }

            return roomsList;
        }

        public async Task<Room> GetByIdAsync(string Id)
        {
            var room = await _roomRepo.GetByIdAsync(ObjectId.Parse(Id));
            return RoomEntity.ConvertToModel(room);
        }
        public async Task<bool> UpdateAsync(Room room)
        {
            return await _roomRepo.UpdateAsync(Room.ConvertToEntity(room));
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _roomRepo.DeleteAsync(id);
        }

    }
}

