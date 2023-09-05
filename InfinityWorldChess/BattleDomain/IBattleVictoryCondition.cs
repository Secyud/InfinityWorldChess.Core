namespace InfinityWorldChess.BattleDomain
{
	public interface IBattleVictoryCondition : IOnBattleInitialize
	{
		public string Description { get; }
		
		public bool Victory { get; }
		
		public bool Defeated { get; }
	}
}