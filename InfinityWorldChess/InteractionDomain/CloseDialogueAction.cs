using Secyud.Ugf;

namespace InfinityWorldChess.InteractionDomain
{
    public class CloseDialogueAction:IDialogueAction
    {
        public string ActionText { get; set; } = "结束对话";
        
        public bool Visible()
        {
            return true;
        }

        public void Invoke()
        {
            U.Get<DialogueService>().CloseDialoguePanel();
        }
    }
}