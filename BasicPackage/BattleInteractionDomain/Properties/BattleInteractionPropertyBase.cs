#region

using System;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.BattleInteractionDomain
{
    public abstract class BattleInteractionPropertyBase :
        IInstallable<BattleInteraction>,IOverlayable<BattleInteraction>,IHasId<Type>
    {
        public virtual Type Id => GetType();
        
        public virtual void InstallFrom(BattleInteraction target)
        {
        }

        public virtual void UnInstallFrom(BattleInteraction target)
        {
        }

        public virtual void Overlay(IOverlayable<BattleInteraction> otherOverlayable)
        {
        }

        protected static float O(float i,float m = 0)
        {
            return Math.Max(i, m);
        }
    }
}