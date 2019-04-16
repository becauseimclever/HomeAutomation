using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Models
{
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Device> Devices { get; set; }
    }
}
