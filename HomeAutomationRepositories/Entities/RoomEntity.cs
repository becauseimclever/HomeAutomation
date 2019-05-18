using HomeAutomationRepositories.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Entities
{
    [BsonIgnoreExtraElements]
    public class RoomEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<DeviceEntity> Devices { get; set; }
        public static Room ConvertToModel(RoomEntity roomEntity)
        {
            Room newRoom = new Room()
            {
                Id = roomEntity.Id.ToString(),
                Name = roomEntity.Name,
                Devices = new List<Device>()
            };
            foreach (var device in roomEntity.Devices)
            {
                newRoom.Devices.Add(DeviceEntity.ConvertToModel(device));
            }
            return newRoom;
        }
    }
}
