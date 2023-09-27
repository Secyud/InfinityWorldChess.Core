#region

using JetBrains.Annotations;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.BuffDomain
{
    public interface IBuff<TTarget> : IHasId<int>
    {
        void Install([NotNull] TTarget target);

        void UnInstall([NotNull] TTarget target);

        void Overlay([NotNull] IBuff<TTarget> finishBuff);
    }
}