using System;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public abstract class BattleRecordProperty: IBattleRoleProperty
    {
        public virtual Type Id => GetType();
        
        public virtual void InstallFrom(BattleRole target)
        {
        }

        public virtual void UnInstallFrom(BattleRole target)
        {
        }

        public void Overlay(IOverlayable<BattleRole> otherOverlayable)
        {
            throw new UgfException($"{GetType()} cannot be overlapped!");
        }
    }
}