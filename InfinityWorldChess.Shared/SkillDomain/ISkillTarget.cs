#region

using InfinityWorldChess.BattleDomain;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillTarget
	{
		IBattleChess[] Value { get; }
	}
}