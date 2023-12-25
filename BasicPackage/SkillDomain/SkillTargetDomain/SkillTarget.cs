#region

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public class SkillTarget : ISkillTarget, IObjectAccessor<List<BattleUnit>>
	{
		private SkillTarget(IEnumerable<BattleUnit> chesses)
		{
			Value = chesses.ToList();
		}

		public List<BattleUnit> Value { get; }

		public static SkillTarget GetFixedTarget(params BattleUnit[] chesses)
		{
			return new SkillTarget(chesses);
		}

		public static SkillTarget CreateFromRange(ISkillRange range,Func<BattleUnit, bool> condition)
		{
			return new SkillTarget(range.Value
				.Select(u => u.Unit as BattleUnit)
				.Where(condition));
		}
	}
}