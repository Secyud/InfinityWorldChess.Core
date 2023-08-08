using System.IO;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.ArchivingDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class ArchivingScope:DependencyScopeProvider
    {
        public static IMonoContainer<GameLoadPanel> GameLoadPanel;
        
        public static ArchivingScope Instance { get; private set; }


        public ArchivingScope(IwcAb ab)
        {
            GameLoadPanel ??= MonoContainer<GameLoadPanel>.Create(ab);
            
        }

        public void OpenGameLoadPanel()
        {
            GameLoadPanel.Create();
        }
        
        public void CloseGameLoadPanel()
        {
            GameLoadPanel.Destroy();
        }

        public void SaveSlotMessage()
        {
            using FileStream stream = File.OpenWrite(
                Path.Combine(SharedConsts.SavePath,
                SharedConsts.SaveFolder.ToString(), "slot.binary"));
            using DefaultArchiveWriter writer = new (stream);
            GameScope.Instance.Player.Role.Basic.Save(writer);
        }
    }
}