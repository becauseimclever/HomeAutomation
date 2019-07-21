using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace HomeAutomationRepositories.Entities

{
    [BsonDiscriminator("powerstripentity")]
    public class PowerStripEntity : DeviceEntity
    {
        public List<bool> outlets { get; set; }
    }
}
