using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.FormSkills
{
	public class FormSkillTemplate : ActiveSkillBase, IFormSkill
	{
		[field: S(ID = 9)]public byte Type { get; set; }

		[field: S(ID = 10)]public byte State { get; set; }


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