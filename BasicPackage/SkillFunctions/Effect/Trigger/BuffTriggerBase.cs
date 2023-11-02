using System;
using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillFunctions.Effect
{
    public abstract class BuffTriggerBase : IBuffEffect
    {
        [field: S] public List<ITriggerEffect> Effects { get; set; }

        public virtual string Description
        {
            get { return
                Effects
                .Select(u => u.Description)
                .JoinAsString(""); }
        }

        public virtual void Install(BattleRole target, IBuff<BattleRole> buff)
        {
            foreach (ITriggerEffect effect in Effects)
            {
                effect.Install(target, buff);
            }
        }

        public virtual void UnInstall(BattleRole target, IBuff<BattleRole> buff)
        {
            foreach (ITriggerEffect effect in Effects)
            {
                effect.UnInstall(target, buff);
            }
        }

        public virtual void Overlay(IBuffEffect sameEffect, IBuff<BattleRole> buff)
        {
            if (sameEffect is not BuffTriggerBase trigger)
            {
                return;
            }

            foreach (ITriggerEffect effect in Effects)
            {
                Type type = effect.GetType();
                ITriggerEffect te = trigger.Effects.FirstOrDefault(
                    u => u.GetType() == type);
                if (te is not null)
                {
                    effect.Overlay(te, buff);
                }
            }
        }

        public virtual void SetSkill(IActiveSkill skill)
        {
            
            foreach (ITriggerEffect effect in Effects)
            {
                effect.SetSkill(skill);
            }
        }
    }
}