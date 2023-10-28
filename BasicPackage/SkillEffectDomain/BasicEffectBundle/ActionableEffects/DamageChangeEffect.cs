#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.SkillDomain.AttackDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
	public abstract class DamageChangeEffect : IActionableEffect
	{
		[field:S]public float Factor { get; set; }
		
		public virtual string Description => $"伤害变化{Factor:P0}。";

		public int Priority => 1;

		public virtual void Active(SkillInteraction target)
		{
			AttackRecordBuff attack = target.SetAttack();
			attack.DamageFactor += Factor;
		}

		public void Install(BattleRole target, IBuff<BattleRole> buff)
		{
		}

		public void UnInstall(BattleRole target, IBuff<BattleRole> buff)
		{
		}

		public void Overlay(IBuffEffect sameEffect, IBuff<BattleRole> buff)
		{
		}

		public void SetSkill(IActiveSkill skill)
		{
			
		}
	}
}