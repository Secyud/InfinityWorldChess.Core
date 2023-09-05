#region

using JetBrains.Annotations;

#endregion

namespace InfinityWorldChess.BuffDomain
{
    public interface IBuff<TTarget>
    {
        void Install([NotNull] TTarget target);

        void UnInstall([NotNull] TTarget target);

        void Overlay([NotNull] IBuff<TTarget> finishBuff);
    }
}