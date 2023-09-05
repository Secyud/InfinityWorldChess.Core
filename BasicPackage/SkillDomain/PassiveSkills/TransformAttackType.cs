using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.SkillDomain.AttackDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.PassiveSkills
{

		public class TransformAttackType : PassiveSkillTemplate, IOnBattleRoleInitialize,
			IActionable<SkillInteraction>
		{
			[field:S] public int D256 { get; set; }

			public int Priority => 1;

			public override string HideDescription =>
				$"将所有攻击转化为{D256 switch {0 => "无", 1 => "火", 2 => "冰", 3 => "毒", _ => "未知"}}属性。";

			public void OnBattleInitialize(BattleRole chess)
			{
				var e = chess.GetBattleEvents();
				e.PrepareLaunch.Add(this);
			}

			public void Active(SkillInteraction target)
			{
				AttackRecordBuff attack = target.Get<AttackRecordBuff>();
				if (attack is not null)
					attack.AttackType = (AttackType)D256;
			}

			public override void Equip(Role role)
			{
				role.Buffs.BattleInitializes.Add(this);
			}

			public override void UnEquip(Role role)
			{
				role.Buffs.BattleInitializes.Remove(this);
			}
		}
}