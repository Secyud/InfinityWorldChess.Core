using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.InteractionDomain
{
    public class CloseDialogueAction:DialogueActionBase
    {
        public override void Invoke()
        {
            U.Get<DialogueService>().CloseDialoguePanel();
        }
    }
}