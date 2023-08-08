using InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions;
using InfinityWorldChess.BasicBundle.BattleBuffs.Recorders;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using System;

namespace InfinityWorldChess.BasicBundle.BattleBuffs
{
	public class PoisonBuff : BattleShownBuffBase, IDeBuff
	{
		private BattleRole _target;
		public readonly TimeRecorder TimeRecorder;

		public PoisonBuff()
		{
			TimeRecorder = new TimeRecorder(GetType());
		}

		public float AttackValue { get; set; }

		public override bool Visible => true;

		public override string ShowName => "中毒";

		public override string ShowDescription =>
			$"中毒状态: 每回合持续收到中毒伤害，伤害基值{AttackValue}。{TimeRecorder.Description}";


		public override void Install(BattleRole target)
		{
			BattleScope.Instance.Context.RoundBeginAction += CalculateEffect;
			_target = target;
			TimeRecorder.Install(target);
		}

		public override void UnInstall(BattleRole target)
		{
			BattleScope.Instance.Context.RoundBeginAction -= CalculateEffect;
			TimeRecorder.UnInstall();
		}

		public override void Overlay(IBuff<BattleRole> finishBuff)
		{
			if (finishBuff is not PoisonBuff buff)
				return;

			float time1 = buff.TimeRecorder.TimeFinished;
			float time2 = TimeRecorder.TimeFinished;

			float totalValue = (time1 * buff.AttackValue + time2 * AttackValue);

			TimeRecorder.Overlay(buff.TimeRecorder);
			buff.TimeRecorder.TimeFinished = Math.Max(
				1,
				buff.TimeRecorder.TimeFinished - SharedConsts.BattleTimeFactor / 4f
			);

			buff.AttackValue = totalValue / buff.TimeRecorder.TimeFinished;
		}

		private void CalculateEffect()
		{
			float timeInterval = Math.Min(TimeRecorder.TimeFinished,
				BattleScope.Instance.Context.TotalTime - TimeRecorder.TimeRecord);

			SkillInteraction interaction =
				SkillInteraction.Get(Launcher, _target);
			AttackRecordBuff attack = interaction.SetAttack();
			attack.AttackType = AttackType.Firing;
			attack.Attack = AttackValue * timeInterval / SharedConsts.BattleTimeFactor;
			interaction.RunAttack();
		}
	}
}