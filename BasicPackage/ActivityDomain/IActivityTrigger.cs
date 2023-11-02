using InfinityWorldChess.ActivityTemplates;

namespace InfinityWorldChess.ActivityDomain
{
    public interface IActivityTrigger
    {
        void StartActivity(ActivityGroup group,Activity activity);
        void FinishActivity(ActivityGroup group,Activity activity);
    }
}