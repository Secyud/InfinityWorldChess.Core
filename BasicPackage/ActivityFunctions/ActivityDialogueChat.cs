using System.Linq;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.DialogueFunctions;
using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.ActivityFunctions
{
    public class ActivityDialogueChat : IDialogueFunction
    {
        public void SetDialogue(DialogueUnit dialogueUnit)
        {
            Role role = dialogueUnit.RoleAccessor.Value;

            var property = role.Properties.Get<RoleActivityDialogueProperty>();

            if (property.DialogueActions.Any())
            {
                dialogueUnit.OptionList.Add(new DialogueOption()
                {
                    Actionable = new ShowActivityInDialogue(),
                    ShowText = "任务"
                });
            }
        }
    }
}