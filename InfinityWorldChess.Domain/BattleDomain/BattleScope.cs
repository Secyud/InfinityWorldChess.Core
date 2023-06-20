using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
    
    [DependScope(typeof(GlobalScope))]
    public class BattleScope : DependencyScope
    {
        private static MonoContainer<BattleUiComponent> _ui;
        private static MonoContainer<BattleMapComponent> _map;

        public Battle Battle { get; private set; }

        public static BattleContext Context { get; private set; }
        
        public BattleScope(DependencyManager dependencyProvider, IwcAb ab) : base(dependencyProvider)
        {
            _ui ??= MonoContainer<BattleUiComponent>.Create(ab);
            _map ??= MonoContainer<BattleMapComponent>.Create(ab, onCanvas: false);
            _ui.Create();
            _map.Create();
            _map.Value.Grid.HexMapManager = Get<IBattleHexMapManager>();
            Context = Get<BattleContext>();
            Context.Ui = _ui.Value;
            Context.Map = _map.Value;
        }

        public void CreateBattle(Battle battle)
        {
            Context.OnCreation(battle);
        }

        public override void Dispose()
        {
            Context.OnShutDown();
            Context = null;
            _ui.Destroy();
            _map.Destroy();
        }
    }
}