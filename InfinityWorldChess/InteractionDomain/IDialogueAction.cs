using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.InteractionDomain
{
    public interface IDialogueAction
    {
        string ActionText { get; }
        bool Visible();
        void Invoke();
    }
}