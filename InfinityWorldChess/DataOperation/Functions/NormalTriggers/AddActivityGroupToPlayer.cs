using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.DataOperation.Accessors.ActivityAccessors;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DataOperation.Functions.NormalTriggers
{
    /// <summary>
    /// if you want to add activity to role,
    /// use this trigger.
    /// </summary>
    public class AddActivityGroupToPlayer:ITrigger
    {
        [field: S] private ResourceActivityGroup GroupAccessor { get; set; }
        
        public void Invoke()
        {
            PlayerGameContext context = U.Get<PlayerGameContext>();
            context.Activity.AddReceivedActivity(GroupAccessor.Value);
        }
    }
}