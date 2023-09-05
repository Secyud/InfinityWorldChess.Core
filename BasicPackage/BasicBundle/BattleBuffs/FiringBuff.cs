using InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.AttackDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using InfinityWorldChess.Ugf;

namespace InfinityWorldChess.BasicBundle.BattleBuffs
{
	public class FiringBuff : BattleShownBuffBase, IActionable<SkillInteraction>, IDeBuff
	{
		private BattleRole _target;

		private bool _triggerState;

		public float AttackValue { get; set; }

		public int LayerCount { get; set; }

		public int Priority => 65536;

		public override bool Visible => true;

		public override string ShowName => "灼烧";

		public override string ShowDescription => 
			$"灼烧状态: 每次释放技能将会触发{AttackValue}灼烧伤害。({LayerCount})";

		private BattleEventsBuff _record;

		public override void Install(BattleRole target)
		{
			_target = target;
			_record = _target.GetBattleEvents();
			_record.LaunchCallback.Add(this);
		}

		public override void UnInstall(BattleRole target)
		{
			_record.LaunchCallback.Remove(this);
		}

		public override void Overlay(IBuff<BattleRole> finishBuff)
		{
			if (finishBuff is not FiringBuff buff)
				return;

			int layerCount = LayerCount + buff.LayerCount - 1;
			buff.AttackValue = (AttackValue * LayerCount + buff.AttackValue * buff.LayerCount) / layerCount;
			buff.LayerCount = layerCount;
			buff.Launcher = Launcher;
		}


		public void Active(SkillInteraction target)
		{
			if (_triggerState)
				return;

			_triggerState = true;

			SkillInteraction interaction =
				SkillInteraction.Get(Launcher, _target);
			AttackRecordBuff attack = interaction.SetAttack();
			attack.AttackType = AttackType.Magical;
			attack.Attack = AttackValue;
			interaction.RunAttack();

			_triggerState = false;
		}


	}
}