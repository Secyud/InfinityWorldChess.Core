#region

using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	public class TestBattle : Battle
	{
		public override void OnBattleInitialize()
		{
			PlayerGameContext playerContext = Og.Get<GameScope,PlayerGameContext>(); 
			BattleContext battleContext = Scope.Get<BattleContext>();

			HexGrid grid = battleContext.Map.Grid;
			int cellIndex = Random.Range(0, grid.CellCountX * grid.CellCountZ);
			BattleCamp camp = new()
			{
				Name = "玩家",
				Index = 0,
				Color = Color.green
			};
			
			battleContext.AutoInitializeRole(
				playerContext.Role, camp, grid.GetCell(cellIndex).Coordinates, true
			);
		}
		public override int SizeX => 2;

		public override int SizeZ => 2;

		public TestBattle() : base(new VictoryOnBeatAllEnemies())
		{
		}
	}
}