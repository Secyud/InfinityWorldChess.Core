#region

using System;
using InfinityWorldChess.BuffDomain;

#endregion

namespace InfinityWorldChess.SkillDomain
{
    public abstract class SkillInteractionBuffBase : IBuff<SkillInteraction>
    {
        public byte BuffLevel => 0;
        
        public virtual void Install(SkillInteraction target)
        {
        }

        public virtual void UnInstall(SkillInteraction target)
        {
        }


        public virtual void Overlay(IBuff<SkillInteraction> finishBuff)
        {
        }

        public abstract int Id { get; }


        protected static float I(float i)
        {
            return Math.Max(i, 1);
        }

        protected static float O(float i)
        {
            return Math.Max(i, 0);
        }
    }
}