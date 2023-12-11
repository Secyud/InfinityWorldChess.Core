using System.IO;
using Newtonsoft.Json;

namespace InfinityWorldChess.PluginDomain
{
    public class PluginInfo
    {
        public string ModuleAssemblyName { get; set; }
        public string FolderPath { get; set; }
        public bool Disabled { get; set; }
        
        private PluginInfo()
        {
            
        }
        
        public static PluginInfo CreateFromFile(string fileName)
        {
            string jsonStr = File.ReadAllText(fileName);
            PluginInfo info = JsonConvert.DeserializeObject<PluginInfo>(jsonStr);
            return info;
        }
    }
}