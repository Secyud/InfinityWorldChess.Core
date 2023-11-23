#region

#endregion

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;

namespace InfinityWorldChess.SkillDomain
{
	public interface IActiveSkill : IDataResource,IHasSaveIndex,
		ISkillCastCondition,  IActiveSkillEffect,ISkillCastPosition,ISkillCastResult,ISkill
	{
		byte ConditionCode { get;  }

		byte ConditionMask { get;  }
	}
}