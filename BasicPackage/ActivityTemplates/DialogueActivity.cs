using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityTemplates
{
    public class DialogueActivity : ActivityBase, IActivityTrigger, IActionable
    {
        [field: S(4)] public IObjectAccessor<Role> RoleAccessor { get; set; }
        [field: S(4)] public IObjectAccessor<IDialogueUnit> DialogueAccessor { get; set; }
        [field: S(3)] public string ActionText { get; set; }

        private RoleActivityDialogueProperty Property =>
            RoleAccessor?.Value?.Properties.Get<RoleActivityDialogueProperty>();

        private DialogueOption _option;

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);

            Role role = RoleAccessor.Value;
            transform.AddParagraph($"找到{role?.ShowName}{role?.Position.Coordinates}，与之对话。");
        }

        public override void StartActivity(ActivityGroup group)
        {
            StartActivity(group, this);
        }

        public override void FinishActivity(ActivityGroup group)
        {
            FinishActivity(group, this);
        }


        public void StartActivity(ActivityGroup group, IActivity activity)
        {
            if (_option is not null)
            {
                Property?.DialogueActions.Remove(_option);
            }

            _option = new DialogueOption
            {
                Actionable = this,
                ShowText = ActionText
            };
            
            Property?.DialogueActions.Add(_option);
        }

        public void FinishActivity(ActivityGroup group, IActivity activity)
        {
            Property?.DialogueActions.Remove(_option);
        }

        public void Invoke()
        {
            InteractionScope.Instance.DialogueService.Panel
                .SetInteraction(DialogueAccessor.Value);
        }
    }
}