using InfinityWorldChess.ActivityFunctions;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.DialogueFunctions
{
    public class ShowActivityInDialogue:IActionable
    {
        private IDialogueUnit GetRoleActivityDialogue()
        {
            Role role = GameScope.Instance.Role.MainOperationRole;
            RoleActivityDialogueProperty property = role?.Properties.Get<RoleActivityDialogueProperty>();
            DialogueUnit ret = new()
            {
                DefaultAction = new CloseCurrentDialogue()
            };
            
            if (property is not null)
            {
                foreach (DialogueOption dialogueAction in property.DialogueActions)
                {
                    ret.OptionList.Add(dialogueAction);
                }
            }
            
            return ret;
        }

        public void Invoke()
        {
            U.Get<DialogueService>().Panel.SetInteraction(GetRoleActivityDialogue());
        }
    }
}