using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public interface IBattleRoleBuff :
        IShowable, IHasId<int>,
        IInstallable<BattleRole>,
        IOverlayable<BattleRole>,
        IPropertyAttached
    {
        BattleRole Target { get; set; }
        BattleRole Origin { get; set; }
        int BuffRecord { get; set; }
    }
}