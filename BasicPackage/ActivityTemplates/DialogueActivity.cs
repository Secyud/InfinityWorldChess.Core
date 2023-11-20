using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityTemplates
{
    public class DialogueActivity :ActivityBase, IActivityTrigger,IDialogueAction
    {
        [field: S(4)] public IObjectAccessor<Role> RoleAccessor { get; set; }
        [field: S(4)] public IObjectAccessor<IDialogueUnit> DialogueAccessor { get; set; }
        [field: S(3)] public string ActionText { get; set;}

        private RoleActivityDialogueProperty Property =>
            RoleAccessor?.Value?.GetProperty<RoleActivityDialogueProperty>();

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);

            Role role = RoleAccessor.Value;
            transform.AddParagraph($"找到{role?.ShowName}{role?.Position.Coordinates}，与之对话。");
        }

        public override void StartActivity(ActivityGroup group)
        {
            StartActivity(group,this);
        }

        public override void FinishActivity(ActivityGroup group)
        {
            FinishActivity(group,this);
        }


        public void StartActivity(ActivityGroup group, IActivity activity)
        {
            Property?.AddAction(this);
        }

        public void FinishActivity(ActivityGroup group, IActivity activity)
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