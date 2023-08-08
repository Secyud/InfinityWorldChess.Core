using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillCastEffect
	{
		void Cast(BattleRole role, HexCell releasePosition);
	}
}