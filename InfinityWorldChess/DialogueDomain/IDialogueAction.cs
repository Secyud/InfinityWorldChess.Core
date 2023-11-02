using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.DialogueDomain
{
    public interface IDialogueAction
    {
        string ActionText { get; }

        bool VisibleFor(Role role);
        
        void Invoke();
    }
}