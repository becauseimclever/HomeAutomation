using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using HomeAutomationRepositories.Repositories;
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

            _roomRepo = roomRepository;
        }

        public Task<bool> AddDevice(string id, Device device)
        {
            throw new NotImplementedException();
        }

        public async Task<Room> Create(Room room)
        {
            RoomEntity roomEntity = (RoomEntity)_mapper.Map(room, typeof(Room), typeof(RoomEntity));

            var newRoom = await _roomRepo.CreateRoom(roomEntity);
            return ConvertEntitytoModel(newRoom);
        }

        public async Task Delete(string id)
        {
            await _roomRepo.Delete(id);
        }

        public async Task<List<Room>> GetAll()
        {
            var rooms = await _roomRepo.GetAll();
            var roomsList = new List<Room>();
            foreach (var room in rooms)
            {
                roomsList.Add(ConvertEntitytoModel(room));
            }

            return roomsList;
        }

        public async Task<Room> GetById(string Id)
        {
            var room = await _roomRepo.GetById(ObjectId.Parse(Id));
            return ConvertEntitytoModel(room);

        }
        private Room ConvertEntitytoModel(RoomEntity roomEntity)
        {
            var room = new Room();
            room.Name = roomEntity.Name;
            room.Id = roomEntity.Id.ToString();

            if (!(roomEntity == null) && !(roomEntity.Devices == null))
            {
                room.Devices = new List<Device>();
                foreach (var device in roomEntity.Devices)
                {
                    room.Devices.Add(new Device() { Name = device.Name });
                }
            }
            return room;
        }
        private RoomEntity ConvertModeltoEntity(Room room)
        {
            var roomEntity = new RoomEntity()
            {
                Id = ObjectId.Parse(room.Id),
                Name = room.Name
            };
            foreach (var device in room.Devices)
            {
                if (device.GetType() == typeof(PowerStrip))
                {
                    roomEntity.Devices.Add(new PowerStripEntity()
                    {
                        Id = ObjectId.Parse(device.Id),
                        Name = device.Name,
                        outlets = ((PowerStrip)device).outlets

                    });
                }
                else
                {
                    roomEntity.Devices.Add(new DeviceEntity()
                    {
                        Id = ObjectId.Parse(device.Id),
                        Name = device.Name,
                    });
                }
            }
            return roomEntity;
        }

    }
}

