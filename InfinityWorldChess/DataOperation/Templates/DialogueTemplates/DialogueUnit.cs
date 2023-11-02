using System.Collections.Generic;
using InfinityWorldChess.DataOperation.Functions.DialogueActionFunctions;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DataOperation.Templates.DialogueTemplates
{
    public class DialogueUnit : IDialogueUnit
    {
        [S] private readonly List<IDialogueAction> _actions = new();
        public IList<IDialogueAction> ActionList => _actions;
        [field: S] public IDialogueAction DefaultAction { get; set; }
        [field: S] public string Text { get; set; }
        [field: S] public IObjectAccessor<Role> RoleAccessor { get; set; }
    }
}