using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HomeAutomationRepositories.Models
{
    [ExcludeFromCodeCoverage]
    public class PowerStrip : Device
    {
        public List<bool> outlets { get; set; }
    }
}
