using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public interface IRoleBuff : IEquippable<Role>, IOverlayable<Role>, IHasId<int>
    {
    }
}