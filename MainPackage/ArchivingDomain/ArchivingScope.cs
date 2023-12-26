using System.IO;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.ArchivingDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class ArchivingScope : DependencyScopeProvider
    {
        public readonly IMonoContainer<GameLoadPanel> GameLoadPanel;

        public static ArchivingScope Instance { get; private set; }


        public ArchivingScope(IwcAssets assets)
        {
            GameLoadPanel = MonoContainer<GameLoadPanel>.Create(assets);
        }

        public override void OnInitialize()
        {
            Instance = this;
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
                MainPackageConsts.SaveFilePath("slot"));
            using DefaultArchiveWriter writer = new(stream);
            GameScope.Instance.Player.Role.Basic.Save(writer);
        }

        public override void Dispose()
        {
            Instance = null;
        }
    }
}