using InfinityWorldChess.DialogueAccessors;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueFunctions
{
    public class ConductDialogue:IActionable
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