#region

#endregion

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.Archiving;

namespace InfinityWorldChess.SkillDomain
{
	public interface IActiveSkill : IDataResource,IHasSaveIndex,
		ISkillCastCondition,  IActiveSkillEffect,ISkillCastPosition,
		ISkillCastResult,ISkillTargetInRange,ISkill
	{
		byte ConditionCode { get;  }

		byte ConditionMask { get;  }
	}
}