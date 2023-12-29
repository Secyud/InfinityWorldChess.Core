#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	/// <summary>
	/// 被动技能
	/// </summary>
	public interface IPassiveSkill : ISkill,IHasSaveIndex,IDataResource, IPassiveSkillEffect
	{
	}
}