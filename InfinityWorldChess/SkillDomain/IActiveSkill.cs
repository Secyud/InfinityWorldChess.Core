#region

#endregion

using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	public interface IActiveSkill : IHasContent,
		ISkillCastCondition,  IActiveSkillEffect,ISkillCastPosition,ISkillCastResult,ISkill
	{
		
	}
}