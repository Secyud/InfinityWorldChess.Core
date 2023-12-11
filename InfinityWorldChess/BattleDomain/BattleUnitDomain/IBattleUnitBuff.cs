using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public interface IBattleUnitBuff :
        IShowable, IHasId<int>,
        IInstallable<BattleUnit>,
        IOverlayable<BattleUnit>,
        IPropertyAttached
    {
        BattleUnit Target { get; set; }
        BattleUnit Origin { get; set; }
        int BuffRecord { get; set; }
    }
}