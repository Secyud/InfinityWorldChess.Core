#region

using System.Collections.Generic;
using InfinityWorldChess.BattleDomain;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillRange
	{
		List<BattleCell> Value { get; }
	}
}