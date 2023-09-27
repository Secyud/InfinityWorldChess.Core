#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.AttackDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.SkillEffectDomain.BattleBuffs
{
	public  class DefendDamageAddEffect : IBuffEffect, IActionable<SkillInteraction>
	{
		private BattleEventsBuff _recordBuff;
		private BattleRole _target;

		[field:S]public float Factor { get; set; }

		public virtual string ShowDescription => $"受到伤害增加{Factor:P0}。";

		public int Priority => 1;

		public void Install(BattleRole target, IBuff<BattleRole> buff)
		{
			_recordBuff = target.GetBattleEvents();
			_recordBuff.PrepareReceive.Add(this);
		}

		public void UnInstall(BattleRole target, IBuff<BattleRole> buff)
		{
			_recordBuff.PrepareReceive.Remove(this);
		}

		public void Overlay(IBuffEffect thisEffect, IBuff<BattleRole> buff)
		{
			if (thisEffect is not DefendDamageAddEffect effect)
				return;

			if (effect.Factor <= Factor)
				effect.Factor = Factor;
			else
				effect.Factor += (Factor * 32 - effect.Factor * 31) / 32;
		}

		public void Active(SkillInteraction target)
		{
			AttackRecordBuff attack = target.SetAttack();
			attack.DamageFactor += Factor;
		}
	}
}