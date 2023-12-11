using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastCondition:IHasContent
	{
		string CheckCastCondition(BattleUnit chess,IActiveSkill skill = null);
		void ConditionCast(BattleUnit chess,IActiveSkill skill = null);
	}
}