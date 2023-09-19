using Secyud.Ugf;

namespace InfinityWorldChess.ActivityDomain
{
    public interface IActivity:IShowable,IHasContent
    {
        ActivityState State { get; set; }

        void SetActivity(IActivityGroup group);
    }
}