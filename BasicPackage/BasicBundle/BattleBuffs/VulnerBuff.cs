#region

using InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions;
using InfinityWorldChess.BasicBundle.BattleBuffs.Recorders;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.SkillDomain.AttackDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using InfinityWorldChess.Ugf;

#endregion

namespace InfinityWorldChess.BasicBundle.BattleBuffs
{
	public abstract class VulnerBuff : BattleShownBuffBase, IActionable<SkillInteraction>
	{
		private BattleEventsBuff _recordBuff;
		private BattleContext _context;
		private BattleRole _target;

		public float Factor { get; set; }

		public override string ShowName => "易伤";

		public override string ShowDescription => $"受到伤害增加{Factor:P0}。";

		public int Priority => 1;

		public override void Install(BattleRole target)
		{
			_recordBuff = target.GetBattleEvents();
			_recordBuff.PrepareReceive.Add(this);
		}

		public override void UnInstall(BattleRole target)
		{
			_recordBuff.PrepareReceive.Remove(this);
		}

		public override void Overlay(IBuff<BattleRole> finishBuff)
		{
			if (finishBuff is not VulnerBuff buff)
				return;

			if (buff.Factor <= Factor)
				buff.Factor = Factor;
			else
				buff.Factor += (Factor * 32 - buff.Factor * 31) / 32;
		}

		public void Active(SkillInteraction target)
		{
			AttackRecordBuff attack = target.SetAttack();
			attack.DamageFactor += Factor;
		}

		public abstract class WithTimeRecord : VulnerBuff
		{
			public readonly TimeRecorder TimeRecorder;

			public override string ShowDescription => base.ShowDescription + TimeRecorder.Description;

			protected WithTimeRecord()
			{
				TimeRecorder = new TimeRecorder(GetType());
			}

			public override void Install(BattleRole target)
			{
				base.Install(target);
				TimeRecorder.Install(target);
			}


			public override void UnInstall(BattleRole target)
			{
				base.UnInstall(target);
				TimeRecorder.UnInstall();
			}

			public override void Overlay(IBuff<BattleRole> finishBuff)
			{
				base.Overlay(finishBuff);

				if (finishBuff is not WithTimeRecord buff)
					return;

				TimeRecorder.Overlay(buff.TimeRecorder);
			}

		}

		public class TimeRemove : WithTimeRecord
		{
		}
	}
}