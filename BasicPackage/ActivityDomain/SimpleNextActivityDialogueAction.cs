using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ActivityDomain
{
    /// <summary>
    /// simple set activity finished within a dialogue.
    /// </summary>
    public class SimpleNextActivityDialogueAction : IDialogueAction
    {
        public ActivityGroup Group { get; set; }
        [field: S] public string ActionText { get; set; }
        [field: S] public bool Success { get; set; }

        public bool VisibleFor()
        {
            return true;
        }

        public void Invoke()
        {
            Group.SetCurrentActivityFinished(Success);
        }
    }
}