using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.PlayerDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class GameScope : DependencyScopeProvider
    {
        private static IMonoContainer<SystemMenuComponent> _systemMenu;
        private static IMonoContainer<RoleMessageComponent> _roleMessage;
        private static IMonoContainer<WorldMapComponent> _map;
        private static IMonoContainer<WorldUiComponent> _ui;
        private WorldGameContext _worldContext;
        private PlayerGameContext _playerContext;
        private RoleGameContext _roleContext;

        public WorldGameContext World =>
            _worldContext ??= Get<WorldGameContext>();

        public PlayerGameContext Player =>
            _playerContext ??= Get<PlayerGameContext>();

        public RoleGameContext Role =>
            _roleContext ??= Get<RoleGameContext>();

        public static GameScope Instance { get; private set; }

        public GameScope(IwcAb ab)
        {
            _systemMenu ??= MonoContainer<SystemMenuComponent>.Create(ab);
            _roleMessage ??= MonoContainer<RoleMessageComponent>.Create(ab);
            _ui ??= MonoContainer<WorldUiComponent>.Create(ab);
            _map ??= MonoContainer<WorldMapComponent>.Create(ab, onCanvas: false);

            _ui.Create();
            _map.Create();
            _map.Value.Grid.HexMapManager = U.Get<WorldHexMapManager>();

            Instance = this;
        }

        public override void Dispose()
        {
            _systemMenu.Destroy();
            _roleMessage.Destroy();
            _ui.Destroy();
            _map.Destroy();
            Instance = null;
        }

        public static void OnSystemMenuCreation()
        {
            _systemMenu.Create();
        }

        public static void OnSystemMenuShutdown()
        {
            _systemMenu.Destroy();
        }

        public static void OnRoleMessageCreation(Role role, int page)
        {
            RoleMessageComponent menu = _roleMessage.Create();
            _roleMessage.Value.OnInitialize(role);
            menu.SetPage(page);
        }

        public static void OnRoleMessageShutdown()
        {
            _roleMessage.Destroy();
        }

        public static void RefreshRoleMessageMenu()
        {
            if (_roleMessage.Value)
                _roleMessage.Value.Refresh();
        }

        public static void OnContinue()
        {
            _map.Value.Show();
            _ui.Value.gameObject.SetActive(true);
        }

        public static void OnInterrupt()
        {
            _map.Value.Hide();
            _ui.Value.gameObject.SetActive(false);
        }

        public static void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}