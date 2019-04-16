using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Models
{
    public class PowerStrip : Device
    {
        public List<bool> outlets { get; set; }
    }
}
