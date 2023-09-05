using System.Collections.Generic;
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
        public readonly List<WorldUi> WorldUis = new();
        public bool Operability { get; private set; }

        private WorldGameContext _world;
        private PlayerGameContext _player;
        private RoleGameContext _role;

        public WorldGameContext World => _world ??= Get<WorldGameContext>();
        public PlayerGameContext Player => _player ??= Get<PlayerGameContext>();
        public RoleGameContext Role => _role ??= Get<RoleGameContext>();

        public static GameScope Instance { get; private set; }

        public GameScope(IwcAb ab)
        {
            SystemMenu = MonoContainer<SystemMenuPanel>.Create(ab);
            GameMenu = MonoContainer<GameMenuPanel>.Create(ab);
            Map = MonoContainer<WorldMap>.Create(ab, onCanvas: false);
        }

        public override void OnInitialize()
        {
            Instance = this;
            Map.Create();
            Map.Value.Grid.HexMapManager = U.Get<WorldHexMapManager>();
            Map.Value.transform.SetSiblingIndex(0);
        }

        public override void Dispose()
        {
            SystemMenu.Destroy();
            GameMenu.Destroy();
            Map.Destroy();
            foreach (WorldUi ui in WorldUis)
                Object.Destroy(ui.gameObject);
            
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
        }

        public void CloseGameMenu()
        {
            GameMenu.Destroy();
        }

        public void OnContinue()
        {
            Map.Value.Show();
            Operability = false;
            foreach (WorldUi ui in WorldUis)
                ui.gameObject.SetActive(true);
            Operability = true;
        }

        public void OnInterrupt()
        {
            Map.Value.Hide();
            Operability = false;
            for (int i = 0; i < WorldUis.Count; )
            {
                WorldUi ui = WorldUis[i];
                if (ui.Destroyable)
                {
                    Object.Destroy(ui);
                    WorldUis.RemoveAt(i);
                }
                else
                {
                    ui.gameObject.SetActive(false);
                    i++;
                }
            }
            Operability = true;
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