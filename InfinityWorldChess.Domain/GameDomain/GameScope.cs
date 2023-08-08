using InfinityWorldChess.GameDomain.GameMenuDomain;
using InfinityWorldChess.GameDomain.SystemMenuDomain;
using InfinityWorldChess.GameDomain.WorldMapDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using UnityEditor;

namespace InfinityWorldChess.GameDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class GameScope : DependencyScopeProvider
    {
        public static IMonoContainer<SystemMenuComponent> SystemMenu;
        public static IMonoContainer<GameMenuPanel> GameMenu;
        public static IMonoContainer<WorldMap> Map;

        private WorldGameContext _world;
        private PlayerGameContext _player;
        private RoleGameContext _role;

        public WorldGameContext World => _world ??= Get<WorldGameContext>();
        public PlayerGameContext Player => _player ??= Get<PlayerGameContext>();
        public RoleGameContext Role => _role ??= Get<RoleGameContext>();

        public static GameScope Instance { get; private set; }

        public GameScope(IwcAb ab)
        {
            SystemMenu ??= MonoContainer<SystemMenuComponent>.Create(ab);
            GameMenu ??= MonoContainer<GameMenuPanel>.Create(ab);
            Map ??= MonoContainer<WorldMap>.Create(ab, onCanvas: false);
            Map.Create();
            Map.Value.Grid.HexMapManager = U.Get<WorldHexMapManager>();
            Map.Value.transform.SetSiblingIndex(0);

            Instance = this;
        }

        public override void Dispose()
        {
            SystemMenu.Destroy();
            GameMenu.Destroy();
            Map.Destroy();
            Instance = null;
        }

        public static void OpenSystemMenu()
        {
            SystemMenu.Create();
        }

        public static void CloseSystemMenu()
        {
            SystemMenu.Destroy();
        }

        public static void OpenGameMenu()
        {
            GameMenu.Create();
        }

        public static void CloseGameMenu()
        {
            GameMenu.Destroy();
        }

        public static void OnContinue()
        {
            Map.Value.Show();
        }

        public static void OnInterrupt()
        {
            Map.Value.Hide();
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