using Secyud.Ugf;
using Secyud.Ugf.Archiving;

namespace InfinityWorldChess.ActivityDomain
{
    public interface IActivity:IShowable,IHasContent
    {
        ActivityState State { get; set; }

        void SetActivity(ActivityGroup group);
    }
}