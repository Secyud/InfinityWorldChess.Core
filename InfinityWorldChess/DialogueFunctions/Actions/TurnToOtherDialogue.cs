using InfinityWorldChess.DialogueAccessors;
using InfinityWorldChess.DialogueDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueFunctions
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