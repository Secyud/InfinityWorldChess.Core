using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastResult:IHasContent
	{
		ISkillRange GetCastResultRange(BattleUnit role, BattleCell castPosition,IActiveSkill skill = null);
	}
}