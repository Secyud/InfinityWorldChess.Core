namespace InfinityWorldChess.BattleDomain.AiDomain
{
	public abstract class AiActionNode
	{
		public abstract void InvokeAction();

		public abstract int GetScore();
	}
}