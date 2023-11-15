using System.Collections.Generic;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.NormalTriggers
{
    public class ListTrigger:ITrigger
    {
        [field:S]public List<ITrigger> Triggers { get; } = new();
        
        public void Invoke()
        {
            foreach (ITrigger trigger in Triggers)
            {
                trigger.Invoke();
            }
        }
    }
}