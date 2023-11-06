#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleCellDomain;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillRange
	{
		BattleCell[] Value { get; }
	}
}