using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastPosition
	{
		ISkillRange GetCastPositionRange(BattleRole role);
	}
}