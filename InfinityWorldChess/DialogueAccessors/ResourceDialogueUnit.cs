using System;
using InfinityWorldChess.DialogueDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueAccessors
{
    public class ResourceDialogueUnit : IObjectAccessor<IDialogueUnit>
    {
        [field: S] private string DialogueId { get; set; }

        [field: S, TypeLimit(typeof(IDialogueUnit))]
        private Guid ClassId { get; set; }

        public virtual IDialogueUnit Value =>
            U.Tm.ConstructFromResource(ClassId, DialogueId) as IDialogueUnit;
    }
}