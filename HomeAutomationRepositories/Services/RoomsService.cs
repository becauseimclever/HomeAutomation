using HomeAutomationRepositories.Entities;
using VM = HomeAutomationRepositories.ViewModels;
using HomeAutomationRepositories.Repositories.Interfaces;
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
        private readonly IDeviceRepository _deviceRepo;

        public RoomsService(IRoomRepository roomRepository, IDeviceRepository deviceRepository)
        {
            _roomRepo = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _deviceRepo = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public async Task<Room> CreateAsync(Room room)
        {
            var newRoom = await _roomRepo.CreateRoomAsync(room).ConfigureAwait(true);

            return newRoom;
        }
        public async Task<List<Room>> GetAllAsync()
        {
            var rooms = await _roomRepo.GetAllAsync().ConfigureAwait(true);

            return rooms;
        }

        public async Task<Room> GetByIdAsync(string Id)
        {
            var room = await _roomRepo.GetByIdAsync(ObjectId.Parse(Id)).ConfigureAwait(true);
            return room;
        }
        public async Task<VM.Room> GetByIdWithDevicesAsync(string Id)
        {
            var room = await GetByIdAsync(Id).ConfigureAwait(true);
            var list = new List<Device>();
            foreach (var id in room.DeviceIds)
            {
                list.Add(await _deviceRepo.GetDeviceAsync(id).ConfigureAwait(true));
            }
            return new VM.Room() { Id = room.Id, Name = room.Name, Devices = list };
        }
        public async Task<bool> UpdateAsync(Room room)
        {
            return await _roomRepo.UpdateAsync(room).ConfigureAwait(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _roomRepo.DeleteAsync(id).ConfigureAwait(true);
        }

    }
}

