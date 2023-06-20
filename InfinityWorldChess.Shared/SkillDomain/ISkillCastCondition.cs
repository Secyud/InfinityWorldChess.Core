using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastCondition:ICanBeShown,IHasContent
	{
		string CheckCastCondition(RoleBattleChess chess);
	}
}