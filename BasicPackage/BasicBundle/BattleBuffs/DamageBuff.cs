#region

using InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions;
using InfinityWorldChess.BasicBundle.BattleBuffs.Recorders;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;

#endregion

namespace InfinityWorldChess.BasicBundle.BattleBuffs
{
	public abstract class DamageBuff : BattleShownBuffBase, IActionable<SkillInteraction>
	{
		private BattleEventsBuff _recordBuff;
		private RoleBattleChess _target;

		public float Factor { get; set; }

		public override string ShowName => "强攻";

		public override string ShowDescription => $"造成伤害增加{Factor:P0}。";

		public int Priority => 1;

		public override void Install(RoleBattleChess target)
		{
			_recordBuff = target.GetBattleEvents();
			_recordBuff.PrepareLaunch.Add(this);
		}

		public override void UnInstall(RoleBattleChess target)
		{
			_recordBuff.PrepareLaunch.Remove(this);
		}

		public override void Overlay(IBuff<RoleBattleChess> finishBuff)
		{
			if (finishBuff is not VulnerBuff buff)
				return;

			if (buff.Factor <= Factor)
				buff.Factor = Factor;
			else
				buff.Factor += (Factor * 32 - buff.Factor * 31) / 32;
		}

		public virtual void Active(SkillInteraction target)
		{
			AttackRecordBuff attack = target.SetAttack();
			attack.DamageFactor += Factor;
		}

		public abstract class WithTimeRecord : DamageBuff
		{
			public readonly TimeRecorder TimeRecorder;
			
			public override string ShowDescription => base.ShowDescription + TimeRecorder.Description;
			
			protected WithTimeRecord()
			{
				TimeRecorder = new TimeRecorder(GetType());
			}

			public override void Install(RoleBattleChess target)
			{
				base.Install(target);
				TimeRecorder.Install(target);
			}


			public override void UnInstall(RoleBattleChess target)
			{
				base.UnInstall(target);
				TimeRecorder.UnInstall();
			}

			public override void Overlay(IBuff<RoleBattleChess> finishBuff)
			{
				base.Overlay(finishBuff);

				if (finishBuff is not WithTimeRecord buff)
					return;

				TimeRecorder.Overlay(buff.TimeRecorder);
			}
		}
		public abstract class WithTurnRecord : DamageBuff
		{
			public readonly TurnRecorder TurnRecorder;
			
			public override string ShowDescription => base.ShowDescription + TurnRecorder.Description;
			
			protected WithTurnRecord()
			{
				TurnRecorder = new TurnRecorder(GetType());
			}

			public override void Install(RoleBattleChess target)
			{
				base.Install(target);
				TurnRecorder.Install(target);
			}


			public override void UnInstall(RoleBattleChess target)
			{
				base.UnInstall(target);
				TurnRecorder.UnInstall();
			}

			public override void Overlay(IBuff<RoleBattleChess> finishBuff)
			{
				base.Overlay(finishBuff);

				if (finishBuff is not WithTurnRecord buff)
					return;

				TurnRecorder.Overlay(buff.TurnRecorder);
			}
		}

		public abstract class WithTrigRecorder : DamageBuff
		{
			public readonly TrigRecorder TrigRecorder;
			
			public override string ShowDescription => base.ShowDescription + TrigRecorder.Description;
			
			protected WithTrigRecorder()
			{
				TrigRecorder = new TrigRecorder(GetType());
			}

			public override void Install(RoleBattleChess target)
			{
				base.Install(target);
				TrigRecorder.Install(target);
			}

			public override void Overlay(IBuff<RoleBattleChess> finishBuff)
			{
				base.Overlay(finishBuff);

				if (finishBuff is not WithTurnRecord buff)
					return;

				TrigRecorder.Overlay(buff.TurnRecorder);
			}

			public override void Active(SkillInteraction target)
			{
				base.Active(target);
				TrigRecorder.CalculateRemove();
			}
		}

		public class TimeRemove : WithTimeRecord
		{
		}
	}
}