#region

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface IActiveSkill : 
		ISkillCastCondition, ISkillCastPosition, ISkillCastResult, ISkillCastEffect,ISkill
	{
		SkillTargetType TargetType { get; }

		bool Damage { get; }
	}
}