#region

using System.Collections.Generic;
using InfinityWorldChess.BattleDomain;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillTarget
	{
		List<BattleRole> Value { get; }
	}
}