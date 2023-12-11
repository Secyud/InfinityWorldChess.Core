using System;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public interface IBattleUnitProperty : IInstallable<BattleUnit>, IOverlayable<BattleUnit>, IHasId<Type>
    {
    }
}