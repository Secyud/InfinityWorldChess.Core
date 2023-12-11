using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public class StopActionNode : IAiActionNode
    {
        public bool IsInterval => false;

        public bool InvokeAction()
        {
            return false;
        }

        public int GetScore()
        {
            return 1;
        }
    }
}