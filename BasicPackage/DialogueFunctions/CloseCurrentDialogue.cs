using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.DialogueFunctions
{
    public class CloseCurrentDialogue:IActionable
    {
        public void Invoke()
        {
            U.Get<DialogueService>().CloseDialoguePanel();
        }
    }
}