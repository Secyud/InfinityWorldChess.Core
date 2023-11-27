using System.Collections.Generic;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueTemplates
{
    public class ListDialogueUnit : IDialogueUnit, IObjectAccessor<IDialogueUnit>, IActionable
    {
        [field: S(1)] public List<IDialogueUnit> Dialogues { get; } = new();
        [field: S(2)] public IActionable FinishDialogueAction { get; set; }

        private int _currentIndex = 0;
        private IDialogueUnit _currentDialogue;
        public IList<DialogueOption> OptionList => null;
        public IActionable DefaultAction => this;
        public string Text => _currentDialogue?.Text;
        public IObjectAccessor<Role> RoleAccessor => _currentDialogue?.RoleAccessor;
        public IDialogueUnit Value => this;

        public void Invoke()
        {
            if (_currentIndex < Dialogues.Count)
            {
                _currentDialogue = Dialogues[_currentIndex];
                InteractionScope.Instance.DialogueService.Panel.SetInteraction(this);
                _currentIndex++;
            }
            else
            {
                _currentIndex = 0;
                InteractionScope.Instance.DialogueService.CloseDialoguePanel();
                FinishDialogueAction?.Invoke();
            }
        }
    }
}