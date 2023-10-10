using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.ActivityDomain
{
    public class GetActivityListDialogueAction:NextDialogueAction
    {
        public GetActivityListDialogueAction()
        {
            ActionText = "任务";
        }
        protected override IDialogueUnit NextDialogueUnit()
        {
            ActivityContext context = U.Get<ActivityContext>();
            DialogueUnit ret = new();
            ret.ActionList.AddRange(context.DialogueActions);
            return ret;
        }
    }
}