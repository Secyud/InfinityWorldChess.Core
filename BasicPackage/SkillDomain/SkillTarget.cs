#region

using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public class SkillTarget : ISkillTarget, IObjectAccessor<IBattleChess[]>
	{
		private static BattleContext Context => BattleScope.Instance.Context;

		private SkillTarget(IEnumerable<IBattleChess> chesses)
		{
			Value = chesses.ToArray();
		}

		public IBattleChess[] Value { get; }

		public static SkillTarget GetFixedTarget(params IBattleChess[] chesses)
		{
			return new SkillTarget(chesses);
		}

		public static SkillTarget GetEnemies(BattleCamp camp, ISkillRange range)
		{
			return new SkillTarget(
				from cell in range.Value
				where cell.Unit
				select Context.GetChess(cell.Unit)
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
				select Context.GetChess(cell.Unit)
				into c
				where c?.Camp == camp
				select c
			);
		}

		public static ISkillTarget SelfAndTeammates(RoleBattleChess chess, ISkillRange range)
		{
			var camp = chess.Camp;
			List<IBattleChess> r =
				(from cell in range.Value
					where cell.Unit
					select Context.GetChess(cell.Unit)
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
				select Context.GetChess(cell.Unit)
			);
		}

		public static ISkillTarget SelfAndEnemy(RoleBattleChess chess, ISkillRange range)
		{
			BattleCamp camp = chess.Camp;
			List<IBattleChess> r =
				(from cell in range.Value
					where cell.Unit
					select Context.GetChess(cell.Unit)
					into c
					where c?.Camp != camp
					select c).ToList();
			if (r.All(u => u != chess))
				r.Add(chess);
			return new SkillTarget(r);
		}

		public static ISkillTarget All(RoleBattleChess chess, ISkillRange range)
		{
			List<IBattleChess> r =
				(from cell in range.Value
					where cell.Unit
					select Context.GetChess(cell.Unit)).ToList();
			if (r.All(u => u != chess))
				r.Add(chess);
			return new SkillTarget(r);
		}
	}
}