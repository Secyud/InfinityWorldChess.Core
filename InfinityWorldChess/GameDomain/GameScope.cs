using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.GameDomain.GameMenuDomain;
using InfinityWorldChess.GameDomain.SystemMenuDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.GameDomain.WorldMapDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.MessageDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMapExtensions;
using UnityEditor;
using UnityEngine;

namespace InfinityWorldChess.GameDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class GameScope : DependencyScopeProvider
    {
        public readonly IMonoContainer<SystemMenuPanel> SystemMenu;
        public readonly IMonoContainer<GameMenuPanel> GameMenu;


        private readonly IMonoContainer<PointDivisionPanel> _pointPanel;
        private readonly IMonoContainer<WorldMap> _map;


        public WorldGameContext World => Get<WorldGameContext>();
        public PlayerGameContext Player => Get<PlayerGameContext>();
        public RoleGameContext Role => Get<RoleGameContext>();
        public WorldMap Map => _map.Value;

        public static GameScope Instance { get; private set; }

        public GameScope(IwcAssets assets)
        {
            SystemMenu = MonoContainer<SystemMenuPanel>.Create(assets);
            GameMenu = MonoContainer<GameMenuPanel>.Create(assets);
            _map = MonoContainer<WorldMap>.Create(assets, onCanvas: false);
            _pointPanel = MonoContainer<PointDivisionPanel>.Create(assets);
        }

        public override void OnInitialize()
        {
            Instance = this;
            _map.Create();
            _map.Value.Initialize(U.Get<WorldHexGridDrawer>());
            _map.Value.transform.SetSiblingIndex(0);
            U.M.CreateScope<InteractionScope>();
            U.M.CreateScope<MessageScope>();
        }

        public override void Dispose()
        {
            U.M.DestroyScope<InteractionScope>();
            U.M.DestroyScope<MessageScope>();
            
            Map.Destroy();
            Instance = null;
        }

        public WorldCell GetCell(int index)
        {
            return _map.Value.GetCell(index) as WorldCell;
        }

        public WorldCell GetCellR(int x, int z)
        {
            return _map.Value.GetCell(
                x + HexCellExtension.Border, 
                z + HexCellExtension.Border)
                as WorldCell;
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
            _map.Value.Show();
        }

        public void OnInterrupt()
        {
            _map.Value.Hide();
            Transform transform = U.Canvas.transform;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).Destroy();
            }
        }

        public void OpenPointPanel(IAttachProperty property)
        {
            _pointPanel.Create();
            _pointPanel.Value.Bind(property);
        }

        public void ExitGame()
        {
            U.Factory.Shutdown();
        }
    }
}