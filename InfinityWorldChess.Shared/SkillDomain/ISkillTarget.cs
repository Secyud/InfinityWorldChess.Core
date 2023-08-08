#region

using InfinityWorldChess.BattleDomain;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillTarget
	{
		BattleRole[] Value { get; }
	}
}