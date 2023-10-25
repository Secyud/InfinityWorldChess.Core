using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.InteractionDomain
{
    public class SimpleNextDialogueAction:NextDialogueAction
    {
        [field:S] private IDialogueUnit Unit { get; set; } 
        
        protected override IDialogueUnit NextDialogueUnit()
        {
            return Unit;
        }
    }
}