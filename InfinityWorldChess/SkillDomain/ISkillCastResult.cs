using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastResult:IHasDescription
	{
		ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition);
	}
}