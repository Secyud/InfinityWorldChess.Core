using InfinityWorldChess.DialogueAccessors;
using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.NormalTriggers
{
    public class ConductDialogue
    {
        [field: S] public ResourceDialogueUnit UnitAccessor { get; set; }
        
        public void Invoke()
        {
            InteractionScope.Instance.DialogueService.OpenDialoguePanel();
            
            InteractionScope.Instance.DialogueService.Panel
                .SetInteraction(UnitAccessor.Value);
        }
    }
}