using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;

namespace InfinityWorldChess.BattleBuffDomain
{
    public interface IBuffEffect : IInstallable<BattleUnit>,IOverlayable<BattleUnit>
    {
    }
}