namespace InfinityWorldChess.FunctionDomain
{
    public interface IActionable<in TTarget> 
    {
        void Invoke(TTarget target);
    }

    public interface IActionable
    {
        void Invoke();
    }
}