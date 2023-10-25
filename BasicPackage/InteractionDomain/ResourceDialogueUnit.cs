using System;
using System.Collections.Generic;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.InteractionDomain
{
    public class ResourceDialogueUnit:IDialogueUnit
    {
        [field:S] public string ResourceName { get; set; }
        [field:S] public Guid TypeId { get; set; }
        private IDialogueUnit _unit;

        private IDialogueUnit Unit => _unit ??= U.Tm.ConstructFromResource(TypeId, ResourceName) as IDialogueUnit;

        public List<IDialogueAction> ActionList => Unit.ActionList;
        public IDialogueAction DefaultAction => Unit.DefaultAction;
        public string Text => Unit.Text;
    }
}