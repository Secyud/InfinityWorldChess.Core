using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BattleBuffs
{
    public abstract class BuffTriggerBase : IBuffEffect
    {
        [field: S] public IActionableEffect Effect { get; set; }
        public abstract string ShowDescription { get; }

        public virtual void Install(BattleRole target, IBuff<BattleRole> buff)
        {
            Effect.Install(target,buff);
        }

        public virtual void UnInstall(BattleRole target, IBuff<BattleRole> buff)
        {
            Effect.UnInstall(target,buff);
        }

        public virtual void Overlay(IBuffEffect thisEffect, IBuff<BattleRole> buff)
        {
            if (thisEffect is not BuffTriggerBase trigger)
            {
                return;
            }

            Effect.Overlay(trigger.Effect, buff);
        }
    }
}