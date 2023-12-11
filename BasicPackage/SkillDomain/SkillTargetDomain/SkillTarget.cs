#region

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public class SkillTarget : ISkillTarget, IObjectAccessor<List<BattleRole>>
	{
		private SkillTarget(IEnumerable<BattleRole> chesses)
		{
			Value = chesses.ToList();
		}

		public List<BattleRole> Value { get; }

		public static SkillTarget GetFixedTarget(params BattleRole[] chesses)
		{
			return new SkillTarget(chesses);
		}

		public static SkillTarget CreateFromRange(ISkillRange range,Func<BattleRole, bool> condition)
		{
			return new SkillTarget(range.Value
				.Select(u => u.Unit as BattleRole)
				.Where(condition));
		}
	}
}