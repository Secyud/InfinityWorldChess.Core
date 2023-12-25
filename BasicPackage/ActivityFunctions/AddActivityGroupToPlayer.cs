using System.Runtime.InteropServices;
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
    [Guid("100A6A99-2B75-1407-410A-21CC147A78CE")]
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