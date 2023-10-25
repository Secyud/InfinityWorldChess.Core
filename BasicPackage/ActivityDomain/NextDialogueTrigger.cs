using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ActivityDomain
{
    public class NextDialogueTrigger:ITrigger
    {
        [field: S] public IDialogueUnit Unit { get; set; }
        
        public void Invoke()
        {
            InteractionScope.Instance.DialogueService.Panel
                .SetInteraction(Unit);
        }
    }
}