namespace InfinityWorldChess.BuffDomain
{
    public interface IOverlayable<TTarget>
    {
        void Overlay( IBuff<TTarget> finishBuff);
    }
}