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
using UnityEngine;

namespace InfinityWorldChess.GameDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class GameScope : DependencyScopeProvider
    {
        public readonly IMonoContainer<SystemMenuPanel> SystemMenu;
        public readonly IMonoContainer<GameMenuPanel> GameMenu;
        public readonly IMonoContainer<WorldMap> Map;

        public WorldGameContext World =>  Get<WorldGameContext>();
        public PlayerGameContext Player => Get<PlayerGameContext>();
        public RoleGameContext Role => Get<RoleGameContext>();
        
        public static GameScope Instance { get; private set; }

        public GameScope(IwcAssets assets)
        {
            SystemMenu = MonoContainer<SystemMenuPanel>.Create(assets);
            GameMenu = MonoContainer<GameMenuPanel>.Create(assets);
            Map = MonoContainer<WorldMap>.Create(assets, onCanvas: false);
        }

        public override void OnInitialize()
        {
            Instance = this;
            Map.Create();
            Map.Value.Initialize(U.Get<WorldHexGridDrawer>());
            Map.Value.transform.SetSiblingIndex(0);
        }

        public override void Dispose()
        {
            Map.Destroy();
            Instance = null;
        }

        public void OpenSystemMenu()
        {
            SystemMenu.Create();
        }

        public void CloseSystemMenu()
        {
            SystemMenu.Destroy();
        }

        public void OpenGameMenu()
        {
            GameMenu.Create();
            Role role = Player.Role;
            Role.MainOperationRole = role;
        }

        public void CloseGameMenu()
        {
            GameMenu.Destroy();
        }

        public void OnContinue()
        {
            Map.Value.Show();
        }

        public void OnInterrupt()
        {
            Map.Value.Hide();
            Transform transform = U.Canvas.transform;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).Destroy();
            }
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}