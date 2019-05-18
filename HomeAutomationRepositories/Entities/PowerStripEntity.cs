using HomeAutomationRepositories.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Entities
{
    [BsonDiscriminator("powerstripentity")]
    class PowerStripEntity : DeviceEntity
    {
        public List<bool> outlets { get; set; }
        public static PowerStrip ConvertToModel(PowerStripEntity powerStripEntity)
        {
            return new PowerStrip() { Id = powerStripEntity.Id.ToString(), Name = powerStripEntity.Name, outlets = powerStripEntity.outlets };
        }

    }
}
