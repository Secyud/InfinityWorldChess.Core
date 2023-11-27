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
    }
}