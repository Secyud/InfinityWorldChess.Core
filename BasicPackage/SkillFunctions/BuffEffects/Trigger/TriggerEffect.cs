using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.SkillFunctions
{
    public abstract class TriggerEffect : IBuffEffect
    {
        protected BattleContext Context => BattleScope.Instance.Context;

        public event Action ExtraAction;

        protected void ExtraActionInvoke()
        {
            ExtraAction?.Invoke();
        }

        protected SkillBuff SkillBuff { get; set; }

        public virtual SkillBuff BelongBuff
        {
            get => SkillBuff;
            set => SkillBuff = value;
        }

        /// <summary>
        /// add trigger to some event
        /// </summary>
        /// <param name="target"></param>
        public virtual void Install(BattleRole target)
        {
        }

        /// <summary>
        /// remove trigger to some event
        /// </summary>
        /// <param name="target"></param>
        public virtual void UnInstall(BattleRole target)
        {
        }

        /// <summary>
        /// overlay effect
        /// </summary>
        /// <param name="overlay"></param>
        public virtual void Overlay(IBuff<BattleRole> overlay)
        {
        }
    }
}