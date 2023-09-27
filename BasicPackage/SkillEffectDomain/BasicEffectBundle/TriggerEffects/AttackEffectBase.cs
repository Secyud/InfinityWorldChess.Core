using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using InfinityWorldChess.Ugf;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    public abstract class AttackEffectBase : ITriggerEffect
    {
        private BattleRole _target;

        // to avoid self call
        private bool _triggerState;

        public int Priority => 65535;

        public abstract string ShowDescription { get; }

        public virtual void Install(BattleRole target, IBuff<BattleRole> buff)
        {
            _target = target;
        }

        public void UnInstall(BattleRole target, IBuff<BattleRole> buff)
        {
            
        }

        public virtual void Overlay(IBuffEffect thisEffect, IBuff<BattleRole> buff)
        {
            
        }

        private BattleEventsBuff _record;

        public void Active()
        {
            if (_triggerState)
                return;

            _triggerState = true;

            SkillInteraction interaction = SkillInteraction.Get(null, _target);
            SetAttack(interaction);
            interaction.RunAttack();

            _triggerState = false;
        }

        protected virtual void SetAttack(SkillInteraction interaction)
        {
        }
    }
}