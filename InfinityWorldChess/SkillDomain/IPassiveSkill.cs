#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface IPassiveSkill :  IHasContent, ISkill,IPassiveSkillEffect
	{
	}
}