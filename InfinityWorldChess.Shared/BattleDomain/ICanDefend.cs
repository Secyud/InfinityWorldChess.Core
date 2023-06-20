namespace InfinityWorldChess.BattleDomain
{
	public interface ICanDefend
	{
		float MaxHealthValue { get; }

		float HealthValue { get; set; }

		float DefendValue { get; }
	}
}