using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.InteractionDomain
{
    public abstract class DialogueActionBase:IDialogueAction
    {
        [field: S]public string ActionText { get; set; }
        
        public virtual bool VisibleFor(Role role)
        {
            return true;
        }

        public abstract void Invoke();
    }
}