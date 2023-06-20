using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using Secyud.Ugf.Resource;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.FormSkills
{
	public class FormSkillTemplate : ActiveSkillBase, IFormSkill
	{
		[R(13)]public byte Type { get; set; }

		[R(14)]public byte State { get; set; }


		protected override string HDescription => "没有特殊效果。";

		public override SkillTargetType TargetType => SkillTargetType.None;
		
		public override bool Damage => false;

		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddFormSkillInfo(this);
		}

		public override void Cast(IBattleChess battleChess, HexCell releasePosition)
		{
			base.Cast(battleChess,releasePosition);
			HexDirection direction = releasePosition.DirectionTo(battleChess.Unit.Location);
			battleChess.Unit.Location = releasePosition;
			battleChess.Direction = direction;
		}
	}
}