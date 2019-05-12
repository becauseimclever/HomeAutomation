using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Entities
{
    [BsonIgnoreExtraElements]
    public class UserClaimEntity
    {
        public string macAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> Groups { get; set; }

    }
}
