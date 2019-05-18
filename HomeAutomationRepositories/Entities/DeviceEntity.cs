using HomeAutomationRepositories.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeAutomationRepositories.Entities
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(PowerStripEntity))]
    public class DeviceEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }

        public static Device ConvertToModel(DeviceEntity device)
        {
            return new Device() { Id = device.Id.ToString(), Name = device.Name };
        }
    }
}
