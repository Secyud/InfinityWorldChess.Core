using System.Collections.Generic;
using Secyud.Ugf;

namespace InfinityWorldChess.ActivityDomain
{
    public interface IActivityGroup:IShowable
    {
        List<Activity> FinishedList { get; }
        
        ActivityState State { get; set; }
    }
}