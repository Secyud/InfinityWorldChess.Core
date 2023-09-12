#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace InfinityWorldChess.InteractionDomain
{
    public class DialoguePanel : MonoBehaviour
    {
        [SerializeField] private AvatarEditor LeftRoleAvatar;
        [SerializeField] private AvatarEditor RightRoleAvatar;
        [SerializeField] private UnityEvent<string> SayingText;
        [SerializeField] private SButton NextButton;
        [SerializeField] private LayoutGroupTrigger SelectActionContent;
        [SerializeField] private SImage BackGround;

        public SImage BackImage => BackGround;
        
        public Role LeftRole { get; private set; }
        public Role RightRole { get; private set; }
        
        public void SetLeftRole(Role role)
        {
            LeftRole = role;
            LeftRoleAvatar.OnInitialize(role?.Basic);
        }
        
        public void SetRightRole(Role role)
        {
            RightRole = role;
            RightRoleAvatar.OnInitialize(role?.Basic);
        }

        public void SetInteraction(IDialogueUnit unit)
        {
            if (unit is null)
                InteractionScope.Instance.DialogueService.CloseDialoguePanel();
            else
            {
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
                actions = actions.Where(u => u.Visible()).ToList();

                for (int index = 0; index < actions.Count; index++)
                {
                    IDialogueAction action = actions[index];
                    SelectOptionCell cell = IwcAssets.Instance.SelectOptionCell.Value.Instantiate(content);
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