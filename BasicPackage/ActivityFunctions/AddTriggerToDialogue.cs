using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityFunctions
{
    public class AddTriggerToDialogue : IActionInstallTarget,IHasContent
    {
        [field: S] public IObjectAccessor<Role> RoleAccessor { get; set; }
        [field: S(0)] public string ShowText { get; set; }

        private Role _role;
        private DialogueOption _option;

        public void Install(IActionable actionable)
        {
            _role ??= RoleAccessor.Value;

            if (_role is not null)
            {
                var property = _role.Properties.GetOrCreate<RoleActivityDialogueProperty>();
                _option ??= new DialogueOption()
                {
                    Actionable = actionable,
                    ShowText = ShowText
                };

                property.DialogueActions.Add(_option);
            }
        }

        public void UnInstall(IActionable actionable)
        {
            if (_role is not null)
            {
                var property = _role.Properties.GetOrCreate<RoleActivityDialogueProperty>();
                property.DialogueActions.Remove(_option);
            }
        }

        public void SetContent(Transform transform)
        {
            _role ??= RoleAccessor.Value;

            transform.AddParagraph($"找到{_role.ShowName}({_role.Position.X},{_role.Position.Z})进行对话。");
        }
    }
}