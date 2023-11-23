#region

using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.BuffDomain
{
    public interface IBuff<TTarget> : IEquippable<TTarget>, IOverlayable<TTarget>, IHasId<int>
    {
    }
}