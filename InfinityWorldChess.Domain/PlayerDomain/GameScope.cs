using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;
using UnityEditor;

namespace InfinityWorldChess.PlayerDomain
{
    [DependScope(typeof(GlobalScope))]
    public class GameScope : DependencyScope
    {
        private static IMonoContainer<SystemMenuComponent> _systemMenu;
        private static IMonoContainer<RoleMessageComponent> _roleMessage;
        private static IMonoContainer<WorldMapComponent> _map;
        private static IMonoContainer<WorldUiComponent> _ui;

        public static WorldGameContext WorldGameContext;
        public static PlayerGameContext PlayerGameContext;
        public static RoleGameContext RoleGameContext;
        

        public GameScope(DependencyManager dependencyProvider, IwcAb ab) : base(dependencyProvider)
        {
            _systemMenu ??= MonoContainer<SystemMenuComponent>.Create(ab);
            _roleMessage ??= MonoContainer<RoleMessageComponent>.Create(ab);
            _ui ??= MonoContainer<WorldUiComponent>.Create(ab);
            _map ??= MonoContainer<WorldMapComponent>.Create(ab, onCanvas: false);

            _ui.Create();
            _map.Create();
            _map.Value.Grid.HexMapManager = Get<WorldHexMapManager>();

            WorldGameContext = Get<WorldGameContext>();
            WorldGameContext.Map = _map.Value;
            WorldGameContext.Ui = _ui.Value;
            PlayerGameContext = Get<PlayerGameContext>();
            RoleGameContext = Get<RoleGameContext>();
        }

        public override void Dispose()
        {
            _ui.Destroy();
            _map.Destroy();
            WorldGameContext = null;
            PlayerGameContext = null;
            RoleGameContext = null;
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