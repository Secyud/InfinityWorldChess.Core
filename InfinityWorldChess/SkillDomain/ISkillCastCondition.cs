using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastCondition:IHasContent
	{
		string CheckCastCondition(BattleRole chess,IActiveSkill skill = null);
		void ConditionCast(BattleRole chess,IActiveSkill skill = null);
	}
}