#region

using InfinityWorldChess.BattleDomain;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillRange
	{
		BattleCell[] Value { get; }
	}
}