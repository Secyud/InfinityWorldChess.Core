using System.Collections.Generic;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleAccessors;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueTemplates
{
    public class ListDialogueUnit : IDialogueUnit, IDialogueAction
    {
        [field: S] public IObjectAccessor<Role> RoleAccessor { get; set; }
        [field: S] public string Text { get;set; }
        [field: S] public List<DialogueTuple> List { get; } = new();
        [field: S] public ITrigger FinishDialogueAction { get;set; }

        private int _currentIndex = 0;
        public IList<IDialogueAction> ActionList => null;
        public IDialogueAction DefaultAction => this;

        public string ActionText => null;

        public bool VisibleFor(Role role)
        {
            return true;
        }

        public void Invoke()
        {
            if (_currentIndex < List.Count)
            {
                DialogueTuple tuple = List[_currentIndex];
                DialogueUnit unit = new()
                {
                    Text = tuple.Text,
                    DefaultAction = this,
                    RoleAccessor = tuple.RoleAccessor ?? RoleAccessor 
                };
            
                InteractionScope.Instance.DialogueService.Panel.SetInteraction(unit);
                _currentIndex++;
            }
            else
            {
                _currentIndex = 0;
                if (FinishDialogueAction is not null)
                {
                    FinishDialogueAction.Invoke();
                }
                else
                {
                    InteractionScope.Instance.DialogueService.CloseDialoguePanel();
                }
            }
        }
    }
}