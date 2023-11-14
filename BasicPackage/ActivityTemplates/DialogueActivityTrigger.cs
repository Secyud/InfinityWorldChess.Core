using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.DialogueAccessors;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ActivityTemplates
{
    public class DialogueActivityTrigger : IActivityTrigger,IDialogueAction
    {
        [field: S] public IObjectAccessor<Role> RoleAccessor { get; set; }
        [field: S] public ResourceDialogueUnit DialogueAccessor { get; set; }
        [field: S] public string ActionText { get; set;}

        private RoleActivityDialogueProperty Property =>
            RoleAccessor?.Value?.GetProperty<RoleActivityDialogueProperty>();

        public void StartActivity(ActivityGroup group, Activity activity)
        {
            Property?.AddAction(this);
        }

        public void FinishActivity(ActivityGroup group, Activity activity)
        {
            Property?.RemoveAction(this);
        }

        public bool VisibleFor(Role role)
        {
            return true;
        }

        public void Invoke()
        {
            InteractionScope.Instance.DialogueService.Panel.SetInteraction(DialogueAccessor.Value);
        }
    }
}