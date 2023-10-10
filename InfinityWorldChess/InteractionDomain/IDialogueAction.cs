using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.InteractionDomain
{
    public interface IDialogueAction
    {
        string ActionText { get; }
        void Invoke();
    }
}