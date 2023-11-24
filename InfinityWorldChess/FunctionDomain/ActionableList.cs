using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.FunctionDomain
{
    public class ActionableList:IActionable
    {
        [field:S] public List<IActionable> Triggers { get; } = new();
        
        public void Invoke()
        {
            Triggers.InvokeList();
        }
    }
}