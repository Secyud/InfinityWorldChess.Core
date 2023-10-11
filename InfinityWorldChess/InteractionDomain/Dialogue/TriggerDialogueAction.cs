using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.InteractionDomain
{
    /// <summary>
    /// simple set activity finished within a dialogue.
    /// </summary>
    public class TriggerDialogueAction : DialogueActionBase
    {
        [field: S] public ITrigger Trigger { get; set; }
        
        public override void Invoke()
        {
            Trigger.Invoke();
        }
    }
}