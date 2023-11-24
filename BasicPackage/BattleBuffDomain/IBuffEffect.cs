using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;

namespace InfinityWorldChess.BattleBuffDomain
{
    public interface IBuffEffect : IEquippable<BattleRole>,IOverlayable<BattleRole>
    {
    }
}