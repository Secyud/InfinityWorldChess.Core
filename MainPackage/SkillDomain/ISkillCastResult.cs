using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	/// <summary>
	/// 技能释放结果范围
	/// </summary>
	public interface ISkillCastResult:IHasContent
	{
		ISkillRange GetCastResultRange(BattleUnit role, BattleCell castPosition,IActiveSkill skill = null);
	}
}