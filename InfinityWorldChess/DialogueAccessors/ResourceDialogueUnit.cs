using System;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueAccessors
{
    public class ResourceDialogueUnit : ResourceAccessor<IDialogueUnit>
    {
        [field: S, TypeLimit(typeof(IDialogueUnit))]
        private Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
    }
}