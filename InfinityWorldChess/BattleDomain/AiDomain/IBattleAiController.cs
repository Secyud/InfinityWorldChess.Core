using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
	public interface IBattleAiController:IRegistry
	{
		void TryPonder();
	}
}