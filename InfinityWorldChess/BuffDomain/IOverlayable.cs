namespace InfinityWorldChess.BuffDomain
{
    public interface IOverlayable<TTarget>
    {
        /// <summary>
        /// buff level decided witch buff to remain
        /// </summary>
        byte BuffLevel { get; }
        /// <summary>
        /// overlay means use this buff instead of finish buff.
        /// </summary>
        /// <param name="finishBuff"></param>
        void Overlay( IBuff<TTarget> finishBuff);
    }
}