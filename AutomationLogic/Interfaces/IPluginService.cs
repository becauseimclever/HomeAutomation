using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationLogic.Interfaces
{
    public interface IPluginService
    {
        ValueTask<IEnumerable<string>> GetAll();
        ValueTask<(Stream dll, string fileName)> GetPluginAsync(string pluginName);
    }
}
