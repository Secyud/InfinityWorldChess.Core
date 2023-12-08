using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.TableComponents.ButtonComponents;

namespace InfinityWorldChess.LevelDomain
{
    public class TriggerBattleLevelButton:ButtonDescriptor<WorldCell>
    {
        private MonoContainer<BattleLevelPanel> _levelPanel;

        private MonoContainer<BattleLevelPanel> LevelPanel => _levelPanel ??=MonoContainer<BattleLevelPanel>.Create<IwcAssets>();

        public override void Invoke()
        {
            LevelPanel.Create();
        }

        public override string Name => "关卡";
        public override bool Visible(WorldCell target)
        {
            return true;
        }
    }
}