#region

#endregion

using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
	public interface IActiveSkill : IHasContent,
		ISkillCastCondition,  ISkillCastEffect,ISkill
	{
		ISkillRange GetCastPositionRange(BattleRole role);
		ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition);
	}
}