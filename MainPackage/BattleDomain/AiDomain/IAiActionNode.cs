namespace InfinityWorldChess.BattleDomain
{
    public interface IAiActionNode
    {
        bool InvokeAction();
        int Score { get; }
    }
}