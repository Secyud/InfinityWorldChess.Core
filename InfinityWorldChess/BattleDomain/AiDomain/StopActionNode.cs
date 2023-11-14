using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
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