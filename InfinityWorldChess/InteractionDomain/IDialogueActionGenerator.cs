using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.InteractionDomain
{
    /// <summary>
    /// in dialogue, same action may be different for different role.
    /// use generator to generate action or control visibility. 
    /// </summary>
    public interface IDialogueActionGenerator
    {
        bool VisibleFor(Role role);
        IDialogueAction GenerateInteraction(Role role);
    }
}