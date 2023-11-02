using System;
using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DataOperation.Accessors.DialogueAccessors
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