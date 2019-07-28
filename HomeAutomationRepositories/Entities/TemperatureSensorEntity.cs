using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Entities
{
    [BsonDiscriminator("temperaturesensorentity")]
    public class TemperatureSensorEntity : Device
    {
        public float Temp { get; set; }
        public float Humidity { get; set; }
    }
}
