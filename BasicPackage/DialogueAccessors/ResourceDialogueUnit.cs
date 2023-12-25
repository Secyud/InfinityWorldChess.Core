using System;
using System.Runtime.InteropServices;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueAccessors
{
    [Guid("8E54C534-5CC2-F1E6-CC21-88D07F7EC7C4")]
    public class ResourceDialogueUnit : ResourceAccessor<IDialogueUnit>
    {
        [field: S, TypeLimit(typeof(IDialogueUnit))]
        private Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
    }
}