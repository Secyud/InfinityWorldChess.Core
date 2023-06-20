using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastEffect:ICanBeShown,IHasContent,IReleasable
	{
		void Cast(IBattleChess battleChess, HexCell releasePosition);
	}
}