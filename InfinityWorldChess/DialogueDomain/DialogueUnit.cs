using System.Collections.Generic;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueDomain
{
    public class DialogueUnit : IDialogueUnit, IObjectAccessor<IDialogueUnit>
    {
        [field: S(5)] public IList<DialogueOption> OptionList { get; } = new List<DialogueOption>();
        [field: S(4)] public IActionable DefaultAction { get; set; }
        [field: S(1)] public string Text { get; set; }
        [field: S(3)] public IObjectAccessor<Role> RoleAccessor { get; set; }
        public IDialogueUnit Value => this;
    }
}