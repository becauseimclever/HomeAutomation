using HomeAutomationRepositories.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Models
{
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Device> Devices { get; set; }
        public static RoomEntity ConvertToEntity(Room room)
        {
            ObjectId.TryParse(room.Id, out var roomId);
            RoomEntity roomEntity = new RoomEntity()
            {
                Id = roomId,
                Name = room.Name,
                Devices = new List<DeviceEntity>()
            };
            foreach (var device in room.Devices)
            {
                roomEntity.Devices.Add(Device.ConvertToEntity(device));

            }
            return roomEntity;
        }
    }
}
