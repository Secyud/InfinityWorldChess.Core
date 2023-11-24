namespace InfinityWorldChess.FunctionDomain
{
    public interface IOverlayable<TTarget>
    {
        /// <summary>
        /// overlay means use this buff instead of finish buff.
        /// </summary>
        /// <param name="otherOverlayable"></param>
        void Overlay(IOverlayable<TTarget> otherOverlayable);
    }
}