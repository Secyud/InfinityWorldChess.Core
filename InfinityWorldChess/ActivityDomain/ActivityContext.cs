using System.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.ActivityDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class ActivityContext:IRegistry
    {
        public List<IDialogueAction> DialogueActions { get; } = new();
        
        

    }
}