using System;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public abstract class BattleRecordProperty: IBattleRoleProperty
    {
        public virtual Type Id => GetType();
        
        public virtual void Install(BattleRole target)
        {
        }

        public virtual void UnInstall(BattleRole target)
        {
        }

        public void Overlay(IOverlayable<BattleRole> otherOverlayable)
        {
            throw new UgfException($"{GetType()} cannot be overlapped!");
        }
    }
}