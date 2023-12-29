using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	/// <summary>
	/// 技能释放消耗
	/// </summary>
	public interface ISkillCastCondition:IHasContent
	{
		string CheckCastCondition(BattleUnit chess,IActiveSkill skill = null);
		void ConditionCast(BattleUnit chess,IActiveSkill skill = null);
	}
}