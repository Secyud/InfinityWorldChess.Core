using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class BattleScope : DependencyScopeProvider
    {
        private static MonoContainer<BattleUiComponent> _ui;
        private static MonoContainer<BattleMapComponent> _map;
        private BattleContext _context;
        public BattleContext Context => _context ??= Get<BattleContext>();

        public static BattleScope Instance { get; private set; }

        public Battle Battle { get; private set; }

        public BattleScope(IwcAb ab)
        {
            _ui ??= MonoContainer<BattleUiComponent>.Create(ab);
            _map ??= MonoContainer<BattleMapComponent>.Create(ab, onCanvas: false);
            _ui.Create();
            _map.Create();
            Context.Ui = _ui.Value;
            Context.Map = _map.Value;
        }

        public void CreateBattle(Battle battle)
        {
            Context.OnCreation(battle);
            _map.Value.Grid.HexMapManager = Get<IBattleHexMapManager>();
        }

        public override void Dispose()
        {
            Context.OnShutDown();
            Instance = null;
            _ui.Destroy();
            _map.Destroy();
        }
    }
}