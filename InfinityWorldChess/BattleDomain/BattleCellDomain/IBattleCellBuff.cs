using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public interface IBattleCellBuff: IEquippable<BattleCell>, IOverlayable<BattleCell>, IHasId<int>
    {
        
    }
}