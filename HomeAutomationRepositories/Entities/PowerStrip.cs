using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BecauseImClever.HomeAutomationRepositories.Entities

{
    [BsonDiscriminator("powerstrip")]
    public class PowerStrip : Device
    {
        public List<bool> Outlets { get; set; }
    }
}
