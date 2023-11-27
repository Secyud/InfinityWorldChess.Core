using System;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public interface IBattleCellProperty:
        IInstallable<BattleCell>, IOverlayable<BattleCell>, IHasId<Type>
    {
        
    }
}