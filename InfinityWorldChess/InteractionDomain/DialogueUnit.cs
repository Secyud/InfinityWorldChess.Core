using System.Collections.Generic;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.InteractionDomain
{
    public class DialogueUnit : IDialogueUnit
    {
        [field: S] public List<IDialogueAction> ActionList { get; } = new();
        [field: S] public IDialogueAction DefaultAction { get; set; } = new CloseDialogueAction();
        [field: S] public string Text { get; set; }
    }
}