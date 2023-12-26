#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface IPassiveSkill : ISkill,IHasSaveIndex,IDataResource, IPassiveSkillEffect
	{
	}
}