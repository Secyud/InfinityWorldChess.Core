using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public interface IRoleBuff : IInstallable<Role>, IOverlayable<Role>, IHasId<int>
    {
    }
}