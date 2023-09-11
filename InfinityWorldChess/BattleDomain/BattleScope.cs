using InfinityWorldChess.BattleDomain.BattleMapDomain;
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
        private readonly PrefabContainer<HexUnit> _battleUnitPrefab;
        private readonly PrefabContainer<TextMeshPro> _simpleTextMesh;

        public static BattleScope Instance { get; private set; }
        public BattleMap Map => _map.Value;
        public BattleDescriptor BattleDescriptor { get; private set; }
        public IBattleVictoryCondition VictoryCondition { get; private set; }
        public BattleContext Battle => Get<BattleContext>();
        public BattleContext Context =>  Get<BattleContext>();

        public BattleScope(IwcAb ab)
        {
            _map = MonoContainer<BattleMap>.Create(ab, onCanvas: false);
            _battleUnitPrefab = PrefabContainer<HexUnit>.Create(
                ab, U.TypeToPath<BattleContext>() + "Unit.prefab"
            );
            _simpleTextMesh = PrefabContainer<TextMeshPro>.Create(
                ab, "InfinityWorldChess/BattleDomain/DamageText.prefab"
            );
        }

        public override void OnInitialize()
        {
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
            foreach (BattleRole chess in Context.Roles.Values)
                chess.Release();
            Instance = null;
            _map.Destroy();
        }
        
        

        public BattleRole GetChess(HexUnit unit)
        {
            if (!unit) return null;

            Context.Roles.TryGetValue(unit.Id, out BattleRole chess);
            return chess;
        }
        
        public void AddRoleBattleChess(BattleRole chess, HexCell cell)
        {
            HexUnit unit = Object.Instantiate(_battleUnitPrefab.Value, Map.transform);
            unit.Id = Context.GetNextId;
            chess.Unit = unit;
            chess.SetHighlight();
            Map.Grid.AddUnit(chess,unit, cell, 0);
            Context.Roles[unit.Id] = chess;
            chess.Role.OnBattleInitialize(chess);
            foreach (IOnBattleRoleInitialize b in chess.Role.Buffs.BattleInitializes)
                b.OnBattleInitialize(chess);

            if (chess.UnitPlay?.Value)
            {
                HexUnitPlay play = Object.Instantiate(chess.UnitPlay?.Value, unit.transform);
                unit.SetLoopPlay(play);
                Map.StartUnitPlayBroadcast(chess, play, chess.Unit.Location);
            }
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
                            IObjectAccessor<HexUnitPlay> play = role.PassiveSkill[0]?.UnitPlay;

                            BattleRole battleRole = new(role, play)
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
    }
}