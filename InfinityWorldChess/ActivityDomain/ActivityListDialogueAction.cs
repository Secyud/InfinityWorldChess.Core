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
            DialogueService dialogue = U.Get<DialogueService>();
            Role role = dialogue.Panel.RightRole;
            RoleActivityDialogueProperty property = role.GetProperty<RoleActivityDialogueProperty>();
            DialogueUnit ret = new();
            foreach (IDialogueAction dialogueAction in property.DialogueActions)
            {
                ret.ActionList.Add(dialogueAction);
            }
            
            return ret;
        }
    }
}