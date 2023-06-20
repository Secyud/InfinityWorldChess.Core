using System.Collections.Generic;

namespace InfinityWorldChess.ActivityDomain
{
    public interface IActivityGroup
    {
        IActivity Active { get; }
        IList<IActivity> Activities { get; }
        ActivityState State { get; }
        void SetNext(IActivity active);
    }
}