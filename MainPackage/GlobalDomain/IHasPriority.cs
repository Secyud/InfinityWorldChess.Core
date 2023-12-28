namespace InfinityWorldChess.GlobalDomain
{
    /// <summary>
    /// 优先级，在某些action或者buff上可能有优先级，
    /// 届时会通过优先级决定操作顺序或留存。
    /// </summary>
    public interface IHasPriority
    {
        int Priority { get; }
    }
}