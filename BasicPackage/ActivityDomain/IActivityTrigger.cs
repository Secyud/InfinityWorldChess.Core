using InfinityWorldChess.ActivityTemplates;

namespace InfinityWorldChess.ActivityDomain
{
    public interface IActivityTrigger
    {
        void StartActivity(ActivityGroup group,IActivity activity);
        void FinishActivity(ActivityGroup group,IActivity activity);
    }
}