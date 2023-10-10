using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.InteractionDomain
{
    public abstract class NextDialogueAction:IDialogueAction
    {
        [field: S]public string ActionText { get; set; } = string.Empty;

        protected abstract IDialogueUnit NextDialogueUnit();
        
        public virtual bool VisibleFor()
        {
            return true;
        }

        
        public void Invoke()
        {
            DialogueService d = U.Get<DialogueService>();
            d.Panel.SetInteraction(NextDialogueUnit());
        }
    }
}