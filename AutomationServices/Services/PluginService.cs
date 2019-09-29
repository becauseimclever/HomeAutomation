using BecauseImClever.AutomationLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationLogic.Services
{
    public class PluginService : IPluginService
    {
        public async ValueTask<IEnumerable<string>> GetAll()
        {
            var pluginFolder = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
            var pluginPaths = Directory.GetDirectories(pluginFolder);
            var dllPaths = pluginPaths.SelectMany(path =>
            {
                return Directory.GetFiles(path, "*Plugin.dll");
            });
            List<string> dllNames = new List<string>();
            foreach (var path in dllPaths)
            {
                dllNames.Add(Directory.GetParent(path).Name);
            }
            return dllNames;
        }

        public async ValueTask<(Stream dll, string fileName)> GetPluginAsync(string pluginName)
        {
            var pluginFolder = Path.Combine(Directory.GetCurrentDirectory(), "Plugins", pluginName);
            var dllPaths = Directory.GetFiles(pluginFolder, "*Plugin.dll");
            return (new FileStream(dllPaths.First(), FileMode.Open, FileAccess.Read), Path.GetFileName(dllPaths.First()));
        }
    }
}
