#region

using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public class SkillTarget : ISkillTarget, IObjectAccessor<BattleRole[]>
	{
		public SkillTarget(IEnumerable<BattleRole> chesses)
		{
			Value = chesses.ToArray();
		}

		public BattleRole[] Value { get; }

		public static SkillTarget GetFixedTarget(params BattleRole[] chesses)
		{
			return new SkillTarget(chesses);
		}
	}
}