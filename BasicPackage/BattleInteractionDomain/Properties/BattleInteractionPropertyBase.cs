﻿#region

using System;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.BattleInteractionDomain
{
    public abstract class BattleInteractionPropertyBase :
        IEquippable<BattleInteraction>,IOverlayable<BattleInteraction>,IHasId<Type>
    {
        public virtual Type Id => GetType();
        
        public virtual void Install(BattleInteraction target)
        {
        }

        public virtual void UnInstall(BattleInteraction target)
        {
        }

        public virtual void Overlay(IOverlayable<BattleInteraction> otherOverlayable)
        {
        }

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