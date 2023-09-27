using System;
using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    public abstract class BuffTriggerBase : IBuffEffect
    {
        [field: S] public List<ITriggerEffect> Effects { get; set; }

        public virtual string ShowDescription
        {
            get { return
                Effects
                .Select(u => u.ShowDescription)
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

        public virtual void Overlay(IBuffEffect thisEffect, IBuff<BattleRole> buff)
        {
            if (thisEffect is not BuffTriggerBase trigger)
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
    }
}