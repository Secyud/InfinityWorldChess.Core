using Secyud.Ugf;
using Secyud.Ugf.Archiving;

namespace InfinityWorldChess.ActivityDomain
{
    public interface IActivity:IShowable,IHasContent,IDataResource
    {
        ActivityState State { get; set; }
        /// <summary>
        /// add activity trigger,
        /// it will run when received
        /// the activity or game initialize
        /// </summary>
        /// <param name="group"></param>
        void StartActivity(ActivityGroup group);
        void FinishActivity(ActivityGroup group);
    }
}