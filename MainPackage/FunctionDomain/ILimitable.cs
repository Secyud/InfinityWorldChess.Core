namespace InfinityWorldChess.FunctionDomain
{
    /// <summary>
    /// some effect will not run if them doesn't fit some expression.
    /// use actionable limit effect and select effect limit.
    /// </summary>
    public interface ILimitable
    {
        bool CheckUseful();
    }
    /// <summary>
    /// some effect will not run if them doesn't fit some expression.
    /// use actionable limit effect and select effect limit.
    /// </summary>
    public interface ILimitable<in TTarget>
    {
        bool CheckUseful(TTarget target);
    }
}