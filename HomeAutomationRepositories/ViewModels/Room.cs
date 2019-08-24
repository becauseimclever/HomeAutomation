using BecauseImClever.HomeAutomationRepositories.Entities;
using MongoDB.Bson;
using System.Collections.Generic;

namespace BecauseImClever.HomeAutomationRepositories.ViewModels
{
    public class Room
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Device> Devices { get; set; }

    }
}
