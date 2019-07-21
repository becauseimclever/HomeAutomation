using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace HomeAutomationRepositories.Entities
{
    [BsonIgnoreExtraElements]
    public class Room
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<DeviceEntity> Devices { get; set; }

    }
}
