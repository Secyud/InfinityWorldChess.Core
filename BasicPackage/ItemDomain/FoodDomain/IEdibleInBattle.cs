using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public interface IEdibleInBattle
	{
		void EatingInBattle(BattleRole role);
	}
}