using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.InteractionDomain
{
    public abstract class NextDialogueAction:DialogueActionBase
    {
        protected abstract IDialogueUnit NextDialogueUnit();
        
        public override void Invoke()
        {
            DialogueService d = U.Get<DialogueService>();
            d.Panel.SetInteraction(NextDialogueUnit());
        }
    }
}