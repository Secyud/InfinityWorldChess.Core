using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastPosition:IHasDescription
	{
		ISkillRange GetCastPositionRange(BattleRole role,IActiveSkill skill = null);
	}
}