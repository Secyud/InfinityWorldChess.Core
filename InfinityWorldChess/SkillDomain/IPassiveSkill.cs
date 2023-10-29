#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface IPassiveSkill :  
		IHasContent, ISkill,IHasSaveIndex,IDataResource,
		IPassiveSkillEffect
	{
	}
}