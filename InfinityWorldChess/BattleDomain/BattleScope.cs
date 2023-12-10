using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMapExtensions;
using Secyud.Ugf.UgfHexMap;
using TMPro;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class BattleScope : DependencyScopeProvider
    {
        private readonly MonoContainer<BattleMap> _map;
        private readonly PrefabContainer<BattleUnit> _battleUnitPrefab;
        private readonly PrefabContainer<TextMeshPro> _simpleTextMesh;
        private readonly PrefabContainer<BattleRoleStateViewer> _stateViewer;

        private readonly MonoContainer<BattleFinishedPanel> _battleFinishPanel;

        public BattleFinishedPanel BattleFinishPanel => _battleFinishPanel.GetOrCreate();

        public static BattleScope Instance { get; private set; }
        public BattleMap Map => _map.Value;
        public IBattleDescriptor Battle { get; private set; }
        public BattleContext Context => Get<BattleContext>();

        public BattleFlowState State
        {
            get => Context.State;
            set => Context.State = value;
        }

        public BattleScope(IwcAssets assets)
        {
            _map = MonoContainer<BattleMap>.Create(assets, onCanvas: false);
            _battleUnitPrefab = PrefabContainer<BattleUnit>.Create(
                assets, U.TypeToPath<BattleContext>() + "Unit.prefab"
            );
            _simpleTextMesh = PrefabContainer<TextMeshPro>.Create(
                assets, "InfinityWorldChess/BattleDomain/DamageText.prefab"
            );
            _stateViewer = PrefabContainer<BattleRoleStateViewer>.Create(
                assets, "InfinityWorldChess/BattleDomain/BattleRoleDomain/BattleRoleStateViewer.prefab");
            _battleFinishPanel
                = MonoContainer<BattleFinishedPanel>.Create(assets);
        }

        public override void OnInitialize()
        {
            Instance = this;
            _map.Create();
            _map.Value.Initialize(Get<BattleHexGridDrawer>());
        }

        public BattleCell GetCell(int index)
        {
            return _map.Value.GetCell(index) as BattleCell;
        }

        public BattleCell GetCellR(int x, int z)
        {
            return _map.Value.GetCell(
                    x + HexCellExtension.Border, 
                    z + HexCellExtension.Border)
                as BattleCell;
        }

        public static void CreateBattle(IBattleDescriptor descriptor)
        {
            if (descriptor is null)
            {
                return;
            }
            
            GameScope.Instance.OnInterrupt();
            U.M.CreateScope<BattleScope>();

            Instance.Battle = descriptor;

            BattleMap grid = Instance.Map;
            grid.GenerateMap(descriptor.Cell,
                descriptor.SizeX, descriptor.SizeZ);

            descriptor.OnBattleCreated();
        }


        public static void DestroyBattle()
        {
            Transform transform = U.Canvas.transform;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).Destroy();
            }
            
            Instance.Context.OnBattleFinished();
            Instance.Battle.OnBattleFinished();

            U.M.DestroyScope<BattleScope>();
            GameScope.Instance.OnContinue();
        }

        public override void Dispose()
        {
            foreach (BattleRole chess in Context.Roles)
                chess.Release();
            _map.Destroy();
            Instance = null;
        }

        public BattleRole GetChess(HexUnit unit)
        {
            return !unit ? null : Context.Roles.FirstOrDefault(u => u.Unit == unit);
        }

        public void AddRoleBattleChess(BattleRole chess, HexCell cell)
        {
            if (chess.Unit)
            {
                chess.Unit.Destroy();
            }

            BattleUnit unit = Object.Instantiate(_battleUnitPrefab.Value, Map.transform);

            unit.Grid = Map;

            Map.AddUnit(unit, cell, 0);
            Context.Roles.AddIfNotContains(chess);

            unit.SetProperty(chess);

            if (chess.UnitPlay?.Value)
            {
                HexUnitAnim anim = Object.Instantiate(chess.UnitPlay?.Value, unit.transform);
                anim.Play(chess.Unit as UgfUnit, chess.Unit.Location as BattleCell);
            }

            BattleRoleStateViewer viewer = _stateViewer.Instantiate(Map.Ui.transform);
            viewer.TargetTrans = unit.transform;
            viewer.Bind(chess);
            unit.StateViewer = viewer;
            
            Context.OnChessAdded();
        }

        public void RemoveRoleBattleChess(BattleRole chess)
        {
            if (chess.Unit)
            {
                chess.Unit.Destroy();
            }
            chess.Dead = true;
            Context.Roles.Remove(chess);
            Context.OnChessRemoved();
        }

        public void CreateNumberText(HexCell cell, int value, Color color)
        {
            TextMeshPro t = _simpleTextMesh.Value.Instantiate();
            t.text = value.ToString();
            t.color = color;
            Map.AddBillBoard(cell, t.transform);
        }
        //
        // public void AutoInitializeRole(Role role, BattleCamp camp, HexCoordinates hexCoordinates, bool playerControl)
        // {
        //     HexGrid grid = Map;
        //     HexCoordinates coordinate = hexCoordinates;
        //     int i = 0, k = 0;
        //     HexDirection j = HexDirection.Ne;
        //
        //     for (; i < 20; i++)
        //     {
        //         for (; j <= HexDirection.Nw; j++)
        //         {
        //             for (; k < i; k++)
        //             {
        //                 HexCell cell = grid.GetCell(coordinate);
        //                 coordinate += j;
        //                 if (cell is not null && !cell.Unit)
        //                 {
        //                     BattleRole battleRole = new(role)
        //                     {
        //                         Camp = camp,
        //                         PlayerControl = playerControl
        //                     };
        //                     AddRoleBattleChess(battleRole, cell);
        //                     goto End;
        //                 }
        //             }
        //
        //             k = 0;
        //         }
        //
        //         j = HexDirection.Ne;
        //         coordinate += HexDirection.W;
        //     }
        //
        //     End: ;
        // }
    }
}