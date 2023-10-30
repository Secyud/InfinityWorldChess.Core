using InfinityWorldChess.GameDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityListDialogueAction:NextDialogueAction
    {
        public ActivityListDialogueAction()
        {
            ActionText = "任务";
        }
        
        protected override IDialogueUnit NextDialogueUnit()
        {
            Role role = GameScope.Instance.Role.MainOperationRole;
            RoleActivityDialogueProperty property = role?.GetProperty<RoleActivityDialogueProperty>();
            DialogueUnit ret = new();
            
            if (property is not null)
            {
                foreach (IDialogueAction dialogueAction in property.DialogueActions)
                {
                    ret.ActionList.Add(dialogueAction);
                }
            }
            
            return ret;
        }
    }
}