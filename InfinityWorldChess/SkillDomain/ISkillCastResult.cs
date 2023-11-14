using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastResult:IHasDescription
	{
		ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition,IActiveSkill skill = null);
	}
}