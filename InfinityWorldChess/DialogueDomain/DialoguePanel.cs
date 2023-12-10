#region

using System.Collections.Generic;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace InfinityWorldChess.DialogueDomain
{
    public class DialoguePanel : MonoBehaviour
    {
        [SerializeField] private AvatarEditor CurrentRoleAvatar;
        [SerializeField] private UnityEvent<string> SayingText;
        [SerializeField] private SelectOptionCell SelectPrefab;
        [SerializeField] private SButton NextButton;
        [SerializeField] private LayoutGroupTrigger SelectActionContent;
        [SerializeField] private SImage BackGround;

        public SImage BackImage => BackGround;

        public void SetCurrentRole(Role role)
        {
            CurrentRoleAvatar.OnInitialize(role?.Basic);
        }

        public void SetInteraction(IDialogueUnit unit)
        {
            if (unit is null)
            {
                InteractionScope.Instance.DialogueService.Panel.Destroy();
            }
            else
            {
                SetCurrentRole(unit.RoleAccessor?.Value);
                SayingText.Invoke(unit.Text);
                SetDefaultAction(unit.DefaultAction);
                SetActionList(unit.OptionList);
            }
        }

        private void SetDefaultAction(IActionable action)
        {
            NextButton.Clear();
            NextButton.Bind(action is null ? Die : action.Invoke);
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void SetActionList(IList<DialogueOption> actions)
        {
            if (actions.IsNullOrEmpty())
            {
                SelectActionContent.gameObject.SetActive(false);
            }
            else
            {
                SelectActionContent.gameObject.SetActive(true);
                RectTransform content = SelectActionContent.PrepareLayout();

                for (int index = 0; index < actions.Count; index++)
                {
                    DialogueOption action = actions[index];
                    SelectOptionCell cell = SelectPrefab.Instantiate(content);
                    cell.OnInitialize(index, action.Invoke, action.ShowText);
                }
            }
        }

        private void SetBackGround(Sprite sprite)
        {
            BackGround.sprite = sprite;
        }
    }
}