using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.DialogueAccessors;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityTemplates
{
    public class DialogueActivityTrigger : IActivityTrigger, IHasContent,IDialogueAction
    {
        [field: S] public IObjectAccessor<Role> RoleAccessor { get; set; }
        [field: S] public ResourceDialogueUnit DialogueAccessor { get; set; }
        [field: S] public string ActionText { get; set;}
        
        private string _description;
        private string Description
        {
            get
            {
                if (_description is null)
                {
                    Role role = RoleAccessor.Value;
                    _description = $"寻找{role.ShowName}，和他谈谈。";
                }

                return _description;
            }
        }

        private RoleActivityDialogueProperty Property =>
            RoleAccessor?.Value?.GetProperty<RoleActivityDialogueProperty>();

        public void SetContent(Transform transform)
        {
            transform.AddParagraph(Description);
        }

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