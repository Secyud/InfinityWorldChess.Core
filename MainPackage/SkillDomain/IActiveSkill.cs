#region

#endregion

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.Archiving;

namespace InfinityWorldChess.SkillDomain
{
	/// <summary>
	/// 主动技能
	/// </summary>
	public interface IActiveSkill : IDataResource,IHasSaveIndex,
		ISkillCastCondition,  IActiveSkillEffect,ISkillCastPosition,
		ISkillCastResult,ISkillTargetInRange,ISkill
	{
		SkillEffectDelegate EffectDelegate { get; }
		
		byte ConditionCode { get;  }

		byte ConditionMask { get;  }
	}
}