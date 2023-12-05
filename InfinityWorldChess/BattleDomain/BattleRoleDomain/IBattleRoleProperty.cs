using System;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public interface IBattleRoleProperty : IInstallable<BattleRole>, IOverlayable<BattleRole>, IHasId<Type>
    {
    }
}