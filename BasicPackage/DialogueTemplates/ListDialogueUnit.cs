using System.Collections.Generic;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.DialogueTemplates
{
    public class ListDialogueUnit : IDialogueUnit, IObjectAccessor<IDialogueUnit>, IActionable
    {
        [field: S(1)] public List<IDialogueUnit> Dialogues { get; } = new();
        [field: S(2)] public IActionable FinishDialogueAction { get; set; }

        private int _currentIndex = 0;
        private IDialogueUnit CurrentDialogue => Dialogues.Count > _currentIndex ? Dialogues[_currentIndex] : null;
        
        public IList<DialogueOption> OptionList => null;
        public IActionable DefaultAction => this;
        public string Text => CurrentDialogue?.Text;
        public IObjectAccessor<Role> RoleAccessor => CurrentDialogue?.RoleAccessor;
        public IDialogueUnit Value => this;

        public void Invoke()
        {
            if (_currentIndex < Dialogues.Count)
            {
                InteractionScope.Instance.DialogueService.Panel.SetInteraction(this);
                _currentIndex++;
            }
            else
            {
                _currentIndex = 0;
                InteractionScope.Instance.DialogueService.Panel.Destroy();
                FinishDialogueAction?.Invoke();
            }
        }
    }
}