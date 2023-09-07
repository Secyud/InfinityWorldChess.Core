#region

#endregion

using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
	public interface IActiveSkill : IHasContent,
		ISkillCastCondition,  IActiveSkillEffect,ISkillCastPosition,ISkillCastResult,ISkill
	{
		
	}
}