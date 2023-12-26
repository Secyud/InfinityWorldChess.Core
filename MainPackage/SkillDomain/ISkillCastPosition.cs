using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastPosition:IHasContent
	{
		ISkillRange GetCastPositionRange(BattleUnit role,IActiveSkill skill = null);
	}
}