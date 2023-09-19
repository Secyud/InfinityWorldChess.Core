using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.InteractionDomain
{
    public interface IDialogueActionGenerator
    {
        bool VisibleFor(Role role);
        IDialogueAction GenerateInteraction(Role role);
    }
}