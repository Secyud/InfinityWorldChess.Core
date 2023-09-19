namespace InfinityWorldChess.BattleDomain.AiDomain
{
    public class StopActionNode:AiActionNode
    {
        public override void InvokeAction()
        {
            BattleScope.Instance.Map.ExitControl();
        }

        public override int GetScore()
        {
            return 8;
        }
    }
}