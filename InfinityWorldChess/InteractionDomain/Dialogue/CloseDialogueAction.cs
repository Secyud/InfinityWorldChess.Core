using Secyud.Ugf;

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