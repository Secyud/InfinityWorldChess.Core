using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityAddTrigger:ITrigger
    {
        [field: S] private string ActivityGroupName { get; set; }
        [field: S] private int StartActivityIndex { get; set; }
        
        public void Invoke()
        {
            ActivityGroup group = U.Tm.ConstructFromResource<ActivityGroup>(ActivityGroupName);
            PlayerGameContext context = U.Get<PlayerGameContext>();
            context.Activity.AddReceivedActivity(group,StartActivityIndex);
        }
    }
}