using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeAutomationRepositories.Entities
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(PowerStripEntity))]
    public class Device
    {

        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
