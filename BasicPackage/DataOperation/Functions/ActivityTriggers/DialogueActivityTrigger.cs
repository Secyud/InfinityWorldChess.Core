using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.DataOperation.Functions.DialogueActionFunctions;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.DataOperation.Functions.ActivityTriggers
{
    public class DialogueActivityTrigger : IActivityTrigger, IHasContent
    {
        [field: S] public InvokeTriggerInDialogue Action { get; set; }
        [field: S] public int RoleId { get; set; }
        public Role Role => GameScope.Instance.Role[RoleId];
        protected virtual string ShowDescription => $"寻找{Role.ShowName}，和他谈谈。";

        public void SetContent(Transform transform)
        {
            transform.AddParagraph(ShowDescription);
        }

        public void StartActivity(ActivityGroup group, Activity activity)
        {
            Role role = Role;
            RoleActivityDialogueProperty property = role.GetProperty<RoleActivityDialogueProperty>();
            property.AddAction(Action);
        }

        public void FinishActivity(ActivityGroup group, Activity activity)
        {
            RoleActivityDialogueProperty property = Role.GetProperty<RoleActivityDialogueProperty>();

            property.RemoveAction(Action);
        }
    }
}