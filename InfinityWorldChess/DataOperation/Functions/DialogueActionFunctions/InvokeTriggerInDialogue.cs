using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DataOperation.Functions.DialogueActionFunctions
{
    /// <summary>
    /// simple set activity finished within a dialogue.
    /// </summary>
    public class InvokeTriggerInDialogue : DialogueActionBase
    {
        [field: S] public ITrigger Trigger { get; set; }
        
        public override void Invoke()
        {
            Trigger.Invoke();
        }
    }
}