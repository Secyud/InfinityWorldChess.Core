using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public interface IEdible
	{
		void Eating(Role role);
	}
}