namespace InfinityWorldChess.Ugf
{
    public interface ITrigger
    {
        void Invoke();
    }
    public interface ITrigger<in TTarget>
    {
        void Invoke(TTarget target);
    }
}