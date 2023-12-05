using System.Collections.Generic;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleAccessors;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueTemplates
{
    public class MainDialogueUnit : IDialogueUnit
    {
        public IList<DialogueOption> OptionList => null;
        public IActionable DefaultAction => null;
        [field: S(0)] public string Text { get; set; }
        [field: S(0)] public bool Player { get; set; }

        public IObjectAccessor<Role> RoleAccessor =>
            Player ? PlayerRoleAccessor.Instance : MainOperationRoleAccessor.Instance;
    }
}