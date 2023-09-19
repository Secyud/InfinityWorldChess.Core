using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.InteractionDomain
{
    public interface IDialogueUnitGenerator
    {
        bool VisibleFor(Role role);
        IDialogueUnit GenerateInteraction(Role role);
    }
}