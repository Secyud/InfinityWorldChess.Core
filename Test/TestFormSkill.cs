using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using System.Linq;
using InfinityWorldChess.BasicBundle;

namespace InfinityWorldChess
{
	public class TestFormSkill : ActiveSkillBase, IFormSkill
	{
		protected override string HDescription=> "可移动至任何地点。";

		public override string CheckCastCondition(RoleBattleChess chess)
		{
			return null;
		}

		public override ISkillRange GetCastPositionRange(IBattleChess battleChess)
		{
			return SkillRange.GetFixedRange(
				Og.Get<BattleScope,BattleContext>().Checkers
					.Where(u => !u.Cell.Unit)
					.Select(u => u.Cell)
			);
		}

		public override void Cast(IBattleChess battleChess, HexCell releasePosition)
		{
			base.Cast(battleChess,releasePosition);
			battleChess.Unit.Location = releasePosition;
		}
		public byte Type { get; set; }

		public byte State { get; set; }
	}
}