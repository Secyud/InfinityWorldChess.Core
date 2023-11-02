using InfinityWorldChess.DialogueDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.DialogueFunctions
{
    public class CloseCurrentDialogue:DialogueActionBase
    {
        public override void Invoke()
        {
            U.Get<DialogueService>().CloseDialoguePanel();
        }
    }
}