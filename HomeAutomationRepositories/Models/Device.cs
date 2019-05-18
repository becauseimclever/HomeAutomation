using System;
using System.Collections.Generic;
using System.Text;
using HomeAutomationRepositories.Entities;
using MongoDB.Bson;

namespace HomeAutomationRepositories.Models
{
    public class Device
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static DeviceEntity ConvertToEntity(Device device)
        {
            ObjectId.TryParse(device.Id, out var devId);
            return new DeviceEntity() { Id = devId, Name = device.Name };
        }
    }
}
