namespace InfinityWorldChess.BattleDomain
{
    public interface IAiActionNode
    {
        bool IsInterval { get; }
        bool InvokeAction();
        int GetScore();
    }
}