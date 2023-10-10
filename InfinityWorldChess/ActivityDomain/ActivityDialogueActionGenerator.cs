using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityDialogueActionGenerator:IDialogueActionGenerator
    {
        public bool VisibleFor(Role role)
        {
            return true;
        }

        public IDialogueAction GenerateInteraction(Role role)
        {
            return new GetActivityListDialogueAction();
        }
    }
}