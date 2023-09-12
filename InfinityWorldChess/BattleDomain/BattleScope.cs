using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.BattleDomain.BattleMapDomain;
using InfinityWorldChess.BattleDomain.BattleRoleDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Generator;
using Secyud.Ugf.HexMap.Utilities;
using TMPro;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class BattleScope : DependencyScopeProvider
    {
        private readonly MonoContainer<BattleMap> _map;
        private readonly MonoContainer<BattlePlayerController> _controller;
        private readonly PrefabContainer<HexUnit> _battleUnitPrefab;
        private readonly PrefabContainer<TextMeshPro> _simpleTextMesh;

        public static BattleScope Instance { get; private set; }
        public BattleMap Map => _map.Value;
        public BattleDescriptor BattleDescriptor { get; private set; }
        public IBattleVictoryCondition VictoryCondition { get; private set; }
        public BattleContext Battle => Get<BattleContext>();
        public BattleContext Context => Get<BattleContext>();
        public BattleFlowState State { get; set; } = BattleFlowState.OnRound;

        public BattleScope(IwcAssets assets)
        {
            _map = MonoContainer<BattleMap>.Create(assets, onCanvas: false);
            _controller = MonoContainer<BattlePlayerController>.Create(assets);
            _battleUnitPrefab = PrefabContainer<HexUnit>.Create(
                assets, U.TypeToPath<BattleContext>() + "Unit.prefab"
            );
            _simpleTextMesh = PrefabContainer<TextMeshPro>.Create(
                assets, "InfinityWorldChess/BattleDomain/DamageText.prefab"
            );
        }

        public override void OnInitialize()
        {
            Instance = this;
            _map.Create();
            _map.Value.Grid.HexMapManager = U.Get<BattleHexMapManager>();
        }

        internal void CreateBattle(BattleDescriptor descriptor)
        {
            BattleDescriptor = descriptor;
            VictoryCondition = descriptor.GenerateVictoryCondition();

            int width = descriptor.SizeX * HexMetrics.ChunkSizeX;
            int height = descriptor.SizeZ * HexMetrics.ChunkSizeZ;
            HexGrid grid = Map.Grid;

            if (descriptor.Cell is not null)
            {
                HexMapGenerator mapGenerator = U.Get<HexMapGenerator>();
                mapGenerator.Parameter = descriptor.Cell.GetGeneratorParameter(width, height);
                mapGenerator.GenerateMap(grid, width, height);
            }
            else
                grid.CreateMap(width, height);

            descriptor.OnBattleCreated();

            VictoryCondition.OnBattleInitialize();
        }

        public override void Dispose()
        {
            foreach (BattleRole chess in Context.Roles)
                chess.Release();
            Instance = null;
            _map.Destroy();
        }

        public BattleRole GetChess(HexUnit unit)
        {
            return !unit ? null : Context.Roles.FirstOrDefault(u => u.Unit == unit);
        }

        public void AddRoleBattleChess(BattleRole chess, HexCell cell)
        {
            if (chess.Unit)
                chess.Unit.Destroy();
            HexUnit unit = Object.Instantiate(_battleUnitPrefab.Value, Map.transform);
            Map.Grid.AddUnit(chess, unit, cell, 0);
            Context.Roles.AddIfNotContains(chess);
            chess.Unit = unit;
            chess.SetHighlight();
            chess.OnBattleInitialize();
            chess.Dead = false;
            if (chess.UnitPlay?.Value)
            {
                HexUnitPlay play = Object.Instantiate(chess.UnitPlay?.Value, unit.transform);
                unit.SetLoopPlay(play);
                Map.StartUnitPlayBroadcast(chess, play, chess.Unit.Location);
            }

            Context.OnChessAdded();
        }

        public void RemoveRoleBattleChess(BattleRole chess)
        {
            if (chess.Unit)
                chess.Unit.Destroy();
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

        public void AutoInitializeRole(Role role, BattleCamp camp, HexCoordinates hexCoordinates, bool playerControl)
        {
            HexGrid grid = Map.Grid;
            HexCoordinates coordinate = hexCoordinates;
            int i = 0, k = 0;
            HexDirection j = HexDirection.Ne;

            for (; i < 20; i++)
            {
                for (; j <= HexDirection.Nw; j++)
                {
                    for (; k < i; k++)
                    {
                        HexCell cell = grid.GetCell(coordinate);
                        coordinate += j;
                        if (cell && !cell.Unit)
                        {
                            BattleRole battleRole = new(role)
                            {
                                Camp = camp,
                                PlayerControl = playerControl
                            };
                            AddRoleBattleChess(battleRole, cell);
                            goto End;
                        }
                    }

                    k = 0;
                }

                j = HexDirection.Ne;
                coordinate += HexDirection.W;
            }

            End: ;
        }

        public void OpenPlayerControlPanel()
        {
            _controller.Create();
        }
        public void ClosePlayerControlPanel()
        {
            _controller.Destroy();
        }
    }
}