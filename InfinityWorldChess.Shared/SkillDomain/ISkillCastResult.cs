using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastResult:ICanBeShown,IHasContent
	{
		ISkillRange GetCastResultRange(IBattleChess battleChess, HexCell castPosition);
	}
}