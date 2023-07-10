#region

using System.Linq;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	public class VictoryOnBeatAllEnemies : IBattleVictoryCondition
	{
		private BattleContext _context;

		public void OnBattleInitialize()
		{
			BattleScope.Instance.Context.ChessRemoveAction += CheckVictory;
		}

		public string Description => "击败所有不同阵营的角色.";

		public bool Victory { get; private set; }

		public bool Defeated { get; private set; }

		public void CheckVictory()
		{
			bool victory = true;
			bool defeated = true;
			
			foreach (IBattleChess chess in BattleScope.Instance.Context.Chesses.Values
				.Where(chess => chess.Camp is not null))
			{
				if (chess.Camp.Index == 0)
					defeated = false;
				else
					victory = false;
				if (!defeated&&!victory)
					return;
			}
			Victory = victory;
			Defeated = defeated;
		}
	}
}