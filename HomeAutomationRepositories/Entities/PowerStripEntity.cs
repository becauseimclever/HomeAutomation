using HomeAutomationRepositories.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HomeAutomationRepositories.Entities

{
    [BsonDiscriminator("powerstripentity")]
    public class PowerStripEntity : DeviceEntity
    {
        public List<bool> outlets { get; set; }
    }
}
