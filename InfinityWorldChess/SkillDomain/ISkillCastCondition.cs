using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastCondition:IHasDescription
	{
		string CheckCastCondition(BattleRole chess);
		void SkillCastInvoke(BattleRole chess);
	}
}