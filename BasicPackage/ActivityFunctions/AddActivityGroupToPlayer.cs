using InfinityWorldChess.ActivityAccessors;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ActivityFunctions
{
    /// <summary>
    /// if you want to add activity to role,
    /// use this trigger.
    /// </summary>
    public class AddActivityGroupToPlayer : IActionable
    {
        [field: S] private ResourceActivityGroup GroupAccessor { get; set; }
        
        public void Invoke()
        {
            PlayerGameContext context = U.Get<PlayerGameContext>();
            context.Activity.Add(GroupAccessor.Value);
        }
    }
}