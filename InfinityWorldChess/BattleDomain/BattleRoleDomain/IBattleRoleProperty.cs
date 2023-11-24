using System;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public interface IBattleRoleProperty : IEquippable<BattleRole>, IOverlayable<BattleRole>, IHasId<Type>
    {
    }
}