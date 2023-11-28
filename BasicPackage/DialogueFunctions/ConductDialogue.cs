using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueFunctions
{
    public class ConductDialogue:IActionable
    {
        [field: S] public IObjectAccessor<IDialogueUnit> UnitAccessor { get; set; }
        
        public void Invoke()
        {
            InteractionScope.Instance.DialogueService.OpenDialoguePanel();
            
            InteractionScope.Instance.DialogueService.Panel
                .SetInteraction(UnitAccessor.Value);
        }
    }
}