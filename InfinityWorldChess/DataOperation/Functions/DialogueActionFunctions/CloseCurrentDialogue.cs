using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.DataOperation.Functions.DialogueActionFunctions
{
    public class CloseCurrentDialogue:DialogueActionBase
    {
        public override void Invoke()
        {
            U.Get<DialogueService>().CloseDialoguePanel();
        }
    }
}