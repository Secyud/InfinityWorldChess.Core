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
	public abstract class AttackDamageAddEffect : IBuffEffect, IActionable<SkillInteraction>
	{
		private BattleEventsBuff _recordBuff;
		private BattleRole _target;
		
		[field:S]public float Factor { get; set; }
		
		public virtual string ShowDescription => $"造成伤害增加{Factor:P0}。";

		public int Priority => 1;

		public virtual void Install(BattleRole target, IBuff<BattleRole> buff)
		{
			_recordBuff = target.GetBattleEvents();
			_recordBuff.PrepareLaunch.Add(this);
		}

		public virtual void UnInstall(BattleRole target, IBuff<BattleRole> buff)
		{
			_recordBuff.PrepareLaunch.Remove(this);
		}

		public virtual void Overlay(IBuffEffect finishBuff, IBuff<BattleRole> buff)
		{
			if (finishBuff is not AttackDamageAddEffect effect)
				return;

			if (effect.Factor <= Factor)
				effect.Factor = Factor;
			else
				effect.Factor += (Factor * 32 - effect.Factor * 31) / 32;
		}

		public virtual void Active(SkillInteraction target)
		{
			AttackRecordBuff attack = target.SetAttack();
			attack.DamageFactor += Factor;
		}
	}
}