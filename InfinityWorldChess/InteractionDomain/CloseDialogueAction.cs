using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.InteractionDomain
{
    public class CloseDialogueAction:IDialogueAction
    {
        [field: S]public string ActionText { get; set; } = "结束对话";
        
        public bool VisibleFor()
        {
            return true;
        }

        public void Invoke()
        {
            U.Get<DialogueService>().CloseDialoguePanel();
        }
    }
}