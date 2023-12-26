namespace InfinityWorldChess.BattleDomain
{
    public class StopActionNode : IAiActionNode
    {
        public bool InvokeAction()
        {
            return false;
        }

        public int Score => 1;

        public int GetScore()
        {
            return 1;
        }
    }
}