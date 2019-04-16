using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using HomeAutomationRepositories.Repositories;
using MongoDB.Bson;

namespace HomeAutomationRepositories.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly IRoomRepository roomRepo;

        public RoomsService(IRoomRepository roomRepository)
        {
            roomRepo = roomRepository;
        }

        public async Task<bool> AddDevice(string id, Device device)
        {
            var room = await GetById(id);
            room.Devices.Add(device);
            return await roomRepo.Update(ConvertModeltoEntity(room));
        }

        public async Task Create(Room room)
        {
            var roomEntity = new RoomEntity()
            {
                Id = ObjectId.GenerateNewId(),
                Name = room.Name,
                Devices = new List<DeviceEntity>()
            };
            if (room.Devices.Count > 0)
                foreach (var dev in room.Devices)
                {
                    if (dev.GetType() == typeof(PowerStrip))
                    {
                        roomEntity.Devices.Add(new PowerStripEntity()
                        {
                            outlets = ((PowerStrip)dev).outlets
                        });
                    }
                    else
                        roomEntity.Devices.Add(
                            new DeviceEntity()
                            {
                                Name = dev.Name
                            });
                }
            await roomRepo.CreateRoom(roomEntity);
        }

        public async Task Delete(string id)
        {
            await roomRepo.Delete(id);
        }

        public async Task<List<Room>> GetAll()
        {
            var rooms = await roomRepo.GetAll();
            var roomsList = new List<Room>();
            foreach (var room in rooms)
            {
                roomsList.Add(ConvertEntitytoModel(room));
            }

            return roomsList;
        }

        public async Task<Room> GetById(string Id)
        {
            var room = await roomRepo.GetById(ObjectId.Parse(Id));
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

