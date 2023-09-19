using System.Collections.Generic;

namespace InfinityWorldChess.InteractionDomain.ChatDomain
{
    public class ChatDialogueUnit:IDialogueUnit
    {
        public List<IDialogueAction> ActionList { get; } = new();
        public IDialogueAction DefaultAction { get; } = new CloseDialogueAction();
        public string Text => "你找我干什么？";
    }
}