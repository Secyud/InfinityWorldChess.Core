using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public interface IRoleBuff : IInstallable<Role>, IOverlayable<Role>, IHasId<int>
    {
    }
}