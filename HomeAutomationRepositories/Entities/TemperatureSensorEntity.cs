using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Entities
{
    [BsonDiscriminator("temperaturesensorentity")]
    public class TemperatureSensorEntity : DeviceEntity
    {
        public float Temp { get; set; }
        public float Humidity { get; set; }
    }
}
