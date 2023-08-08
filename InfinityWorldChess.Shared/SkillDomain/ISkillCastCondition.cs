using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastCondition
	{
		string CheckCastCondition(BattleRole chess);
	}
}