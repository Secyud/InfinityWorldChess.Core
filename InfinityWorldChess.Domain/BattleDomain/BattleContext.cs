#region

using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.HexMap;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap.Generator;
using Secyud.Ugf.HexMap.Utilities;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	[Registry(LifeTime = DependencyLifeTime.Scoped,DependScope = typeof(BattleScope))]
	public partial class BattleContext 
	{
		private readonly PrefabContainer<HexUnit> _battleUnitPrefab;
		private readonly PrefabContainer<TextMeshPro> _simpleTextMesh;
		private IBattleAiController _controller;
		private IBattleAiController Controller => _controller ??= U.Get<IwcBattleAiController>();
		
		public  BattleMapComponent Map { get; internal set; }
		public  BattleUiComponent Ui { get; internal set; }

		public BattleContext(IwcAb ab)
		{
			_battleUnitPrefab = PrefabContainer<HexUnit>.Create(
				ab, U.TypeToPath<BattleContext>() + "Unit.prefab"
			);
			_simpleTextMesh = PrefabContainer<TextMeshPro>.Create(
				ab, "InfinityWorldChess/BattleDomain/DamageText.prefab"
			);
		}

		internal void OnCreation(Battle battle)
		{
			Width = battle.SizeX * HexMetrics.ChunkSizeX;
			Height = battle.SizeZ * HexMetrics.ChunkSizeZ;
			CellCount = Width * Height;
			Checkers = new BattleChecker[CellCount];
			Battle = battle;

			HexGrid grid = Map.Grid;
			
			if (Battle.Cell is not null)
			{
				HexMapGenerator mapGenerator = U.Get<HexMapGenerator>();
				mapGenerator.Parameter = Battle.Cell.GetGeneratorParameter(Width, Height);
				mapGenerator.GenerateMap(grid, Width, Height);
			}
			else
				grid.CreateMap(Width, Height);

			for (int i = 0; i < CellCount; i++)
				Checkers[i] = new BattleChecker(grid.GetCell(i));

			Battle.OnBattleInitialize();
			Battle.VictoryCondition.OnBattleInitialize();
			
			OnRoundIntervalCalculate();
		}

		internal void OnShutDown()
		{
			foreach (RoleBattleChess chess in Roles)
				chess.Release();
		}

		public BattleChecker GetChecker(HexCell cell)
		{
			if (!cell || cell.Index < 0) return null;

			return Checkers[cell.Index];
		}

		public IBattleChess GetChess(HexUnit unit)
		{
			if (!unit) return null;

			Chesses.TryGetValue(unit.Id, out IBattleChess chess);
			return chess;
		}

		public void AddRoleBattleChess(RoleBattleChess chess, HexCell cell)
		{
			HexUnit unit = Object.Instantiate(_battleUnitPrefab.Value, Map.transform);
			unit.Id = GetNextId;
			unit.DeadAction += OnChessRemove;
			unit.PlayFinishedAction += FinishUnitPlayBroadcast;
			chess.Unit = unit;
			chess.SetHighlight();
			Map.Grid.AddUnit(unit, cell, 0);
			Roles.Add(chess);
			Chesses[unit.Id] = chess;
			chess.Role.OnBattleInitialize(chess);
			foreach (IOnBattleRoleInitialize b in chess.Role.Buffs.BattleInitializes)
				b.OnBattleInitialize(chess);
			
			if (chess.UnitPlay?.Value)
			{
				HexUnitPlay play = Object.Instantiate(chess.UnitPlay?.Value, unit.transform);
				unit.SetLoopPlay(play);
				StartUnitPlayBroadcast(chess,play,chess.Unit.Location);
			}
		}

		public void RemoveBattleChess(IBattleChess chess)
		{
			if (chess.Unit)
			{
				if (chess is RoleBattleChess battleChess)
					battleChess.Release();

				Chesses.Remove(chess.Unit.Id);
				chess.Unit.Die();
			}
		}

		public void CreateText(HexCell cell, int value, Color color)
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

							RoleBattleChess roleBattleChess = new(role,play)
							{
								Camp = camp,
								PlayerControl = playerControl
							};
							AddRoleBattleChess(roleBattleChess,cell);
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