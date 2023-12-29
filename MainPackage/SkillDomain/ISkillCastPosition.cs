using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	/// <summary>
	/// 技能释放位置
	/// </summary>
	public interface ISkillCastPosition:IHasContent
	{
		ISkillRange GetCastPositionRange(BattleUnit role,IActiveSkill skill = null);
	}
}