using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleCellDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastResult:IHasDescription
	{
		ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition,IActiveSkill skill = null);
	}
}