using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastPosition:ICanBeShown,IHasContent
	{
		ISkillRange GetCastPositionRange(IBattleChess battleChess);
	}
}