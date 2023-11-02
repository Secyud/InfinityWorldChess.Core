using InfinityWorldChess.DataOperation.Accessors.DialogueAccessors;
using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DataOperation.Functions.DialogueActionFunctions
{
    public class TurnToOtherDialogue:DialogueActionBase
    {
        [field:S] private ResourceDialogueUnit UnitAccessor { get; set; } 
        
        public override void Invoke()
        {
            DialogueService d = U.Get<DialogueService>();
            d.Panel.SetInteraction(UnitAccessor.Value);
        }
    }
}