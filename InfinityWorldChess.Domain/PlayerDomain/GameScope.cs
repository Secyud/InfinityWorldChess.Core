using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;
using UnityEditor;

namespace InfinityWorldChess.PlayerDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class GameScope : DependencyScopeProvider
    {
        public static IMonoContainer<SystemMenuComponent> SystemMenu;
        public static IMonoContainer<RoleMessageComponent> RoleMessage;
        public static IMonoContainer<WorldMapComponent> Map;
        public static IMonoContainer<WorldUiComponent> UI;
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
            SystemMenu ??= MonoContainer<SystemMenuComponent>.Create(ab);
            RoleMessage ??= MonoContainer<RoleMessageComponent>.Create(ab);
            UI ??= MonoContainer<WorldUiComponent>.Create(ab);
            Map ??= MonoContainer<WorldMapComponent>.Create(ab, onCanvas: false);

            UI.Create();
            Map.Create();
            Map.Value.Grid.HexMapManager = U.Get<WorldHexMapManager>();

            Instance = this;
        }

        public override void Dispose()
        {
            SystemMenu.Destroy();
            RoleMessage.Destroy();
            UI.Destroy();
            Map.Destroy();
            Instance = null;
        }

        public static void OnSystemMenuCreation()
        {
            SystemMenu.Create();
        }

        public static void OnSystemMenuShutdown()
        {
            SystemMenu.Destroy();
        }

        public static void OnRoleMessageCreation(Role role, int page)
        {
            RoleMessageComponent menu = RoleMessage.Create();
            RoleMessage.Value.OnInitialize(role);
            menu.SetPage(page);
        }

        public static void OnRoleMessageShutdown()
        {
            RoleMessage.Destroy();
        }

        public static void RefreshRoleMessageMenu()
        {
            if (RoleMessage.Value)
                RoleMessage.Value.Refresh();
        }

        public static void OnContinue()
        {
            Map.Value.Show();
            UI.Value.gameObject.SetActive(true);
        }

        public static void OnInterrupt()
        {
            Map.Value.Hide();
            UI.Value.gameObject.SetActive(false);
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