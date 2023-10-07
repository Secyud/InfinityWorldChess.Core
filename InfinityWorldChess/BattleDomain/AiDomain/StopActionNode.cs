using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain.AiDomain
{
    public class StopActionNode:AiActionNode
    {
        public override void InvokeAction()
        {
            U.Get<BattleControlService>().ExitControl();
        }

        public override int GetScore()
        {
            return 8;
        }
    }
}