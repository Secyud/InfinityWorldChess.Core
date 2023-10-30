#region

using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.ItemDomain;
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