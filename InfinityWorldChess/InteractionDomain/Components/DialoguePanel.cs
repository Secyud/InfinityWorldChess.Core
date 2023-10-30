#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using System.Collections.Generic;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace InfinityWorldChess.InteractionDomain
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
                InteractionScope.Instance.DialogueService.CloseDialoguePanel();
            else
            {
                GameScope.Instance.Role.TryGetValue(unit.RoleId,out Role role);
                SetCurrentRole(role);
                SayingText.Invoke(unit.Text);
                SetDefaultAction(unit.DefaultAction);
                SetActionList(unit.ActionList);
            }
        }

        private void SetDefaultAction(IDialogueAction action)
        {
            if (action is null)
            {
                NextButton.gameObject.SetActive(false);
            }
            else
            {
                NextButton.gameObject.SetActive(true);
                NextButton.Bind(action.Invoke);
            }
        }

        private void SetActionList( List<IDialogueAction> actions)
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
                    IDialogueAction action = actions[index];
                    SelectOptionCell cell = SelectPrefab.Instantiate(content);
                    cell.OnInitialize(index,action.Invoke , action.ActionText);
                }
            }
        }

        private void SetBackGround(Sprite sprite)
        {
            BackGround.sprite = sprite;
        }
    }
}