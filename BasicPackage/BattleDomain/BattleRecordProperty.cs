using System;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public abstract class BattleRecordProperty: IBattleUnitProperty
    {
        public virtual Type Id => GetType();
        
        public virtual void InstallFrom(BattleUnit target)
        {
        }

        public virtual void UnInstallFrom(BattleUnit target)
        {
        }

        public void Overlay(IOverlayable<BattleUnit> otherOverlayable)
        {
            throw new UgfException($"{GetType()} cannot be overlapped!");
        }
    }
}