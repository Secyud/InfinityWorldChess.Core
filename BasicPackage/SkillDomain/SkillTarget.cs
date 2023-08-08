#region

using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public class SkillTarget : ISkillTarget, IObjectAccessor<BattleRole[]>
	{
		private SkillTarget(IEnumerable<BattleRole> chesses)
		{
			Value = chesses.ToArray();
		}

		public BattleRole[] Value { get; }

		public static SkillTarget GetFixedTarget(params BattleRole[] chesses)
		{
			return new SkillTarget(chesses);
		}

		public static SkillTarget GetEnemies(BattleCamp camp, ISkillRange range)
		{
			return new SkillTarget(
				from cell in range.Value
				where cell.Unit
				select cell.Unit.Get<BattleRole>()
				into chess
				where chess?.Camp != camp
				select chess
			);
		}

		public static ISkillTarget GetTeammates(BattleCamp camp, ISkillRange range)
		{
			return new SkillTarget(
				from cell in range.Value
				where cell.Unit
				select cell.Unit.Get<BattleRole>()
				into c
				where c?.Camp == camp
				select c
			);
		}

		public static ISkillTarget SelfAndTeammates(BattleRole chess, ISkillRange range)
		{
			var camp = chess.Camp;
			List<BattleRole> r =
				(from cell in range.Value
					where cell.Unit
					select cell.Unit.Get<BattleRole>()
					into c
					where c?.Camp == camp
					select c).ToList();
			if (r.All(u => u != chess))
				r.Add(chess);
			return new SkillTarget(r);
		}

		public static ISkillTarget ExcludeSelf(ISkillRange range)
		{
			return new SkillTarget(
				from cell in range.Value
				where cell.Unit
				select cell.Unit.Get<BattleRole>()
			);
		}

		public static ISkillTarget SelfAndEnemy(BattleRole chess, ISkillRange range)
		{
			BattleCamp camp = chess.Camp;
			List<BattleRole> r =
				(from cell in range.Value
					where cell.Unit
					select cell.Unit.Get<BattleRole>()
					into c
					where c?.Camp != camp
					select c).ToList();
			if (r.All(u => u != chess))
				r.Add(chess);
			return new SkillTarget(r);
		}

		public static ISkillTarget All(BattleRole chess, ISkillRange range)
		{
			List<BattleRole> r =
				(from cell in range.Value
					where cell.Unit
					select cell.Unit.Get<BattleRole>())
				.ToList();
			if (r.All(u => u != chess))
				r.Add(chess);
			return new SkillTarget(r);
		}
	}
}