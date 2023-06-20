using InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;

namespace InfinityWorldChess.BasicBundle.BattleBuffs
{
	public class FrozenBuff : BattleShownBuffBase, IActionable<SkillInteraction>, IDeBuff
	{
		private RoleBattleChess _target;

		private bool _triggerState;

		private BattleEventsBuff _record;

		public float AttackValue { get; set; }

		public int LayerCount { get; set; }

		public int Priority => 65536;

		public override bool Visible => true;

		public override string ShowName => "冰冻";

		public override string ShowDescription => 
			$"冰冻状态: 每次受到伤害将会受到{AttackValue}冰冻伤害。({LayerCount})";


		public override void Install(RoleBattleChess target)
		{
			_target = target;
			_record = _target.GetBattleEvents();
			_record.ReceiveCallback.Add(this);
		}

		public override void UnInstall(RoleBattleChess target)
		{
			_record.LaunchCallback.Remove(this);
		}

		public override void Overlay(IBuff<RoleBattleChess> finishBuff)
		{
			if (finishBuff is not FiringBuff buff)
				return;

			int layerCount = LayerCount + buff.LayerCount - 1;
			buff.AttackValue = (AttackValue * LayerCount + buff.AttackValue * buff.LayerCount)
				/ layerCount;

			buff.LayerCount = layerCount;

			buff.Launcher = Launcher;
		}

		public void Active(SkillInteraction target)
		{
			if (_triggerState) return;

			_triggerState = true;
			SkillInteraction interaction =
				SkillInteraction.Get(Launcher, _target);
			AttackRecordBuff attack = interaction.SetAttack();
			attack.AttackType = AttackType.Frozen;
			attack.Attack = AttackValue;
			interaction.RunAttack();
			_triggerState = false;
		}


	}
}