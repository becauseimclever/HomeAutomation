using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BecauseImClever.HomeAutomationRepositories.Entities
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(PowerStrip))]
    public class Device
    {

        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
