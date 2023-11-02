using InfinityWorldChess.DataOperation.Functions.DialogueActionFunctions;
using InfinityWorldChess.DataOperation.Templates.DialogueTemplates;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityListDialogue:DialogueActionBase
    {
        public ActivityListDialogue()
        {
            ActionText = "任务";
        }

        private IDialogueUnit GetRoleActivityDialogue()
        {
            Role role = GameScope.Instance.Role.MainOperationRole;
            RoleActivityDialogueProperty property = role?.GetProperty<RoleActivityDialogueProperty>();
            DialogueUnit ret = new()
            {
                DefaultAction = new CloseCurrentDialogue()
            };
            
            if (property is not null)
            {
                foreach (IDialogueAction dialogueAction in property.DialogueActions)
                {
                    ret.ActionList.Add(dialogueAction);
                }
            }
            
            return ret;
        }

        public override void Invoke()
        {
            DialogueService d = U.Get<DialogueService>();
            d.Panel.SetInteraction(GetRoleActivityDialogue());
        }
    }
}