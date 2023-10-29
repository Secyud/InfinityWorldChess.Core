#region

#endregion

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;

namespace InfinityWorldChess.SkillDomain
{
	public interface IActiveSkill : IHasContent,IDataResource,IHasSaveIndex,
		ISkillCastCondition,  IActiveSkillEffect,ISkillCastPosition,ISkillCastResult,ISkill
	{
		
	}
}