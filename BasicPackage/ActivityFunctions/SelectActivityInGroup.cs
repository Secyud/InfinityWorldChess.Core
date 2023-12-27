using System.Runtime.InteropServices;
using System.Ugf;
using InfinityWorldChess.ActivityAccessors;
using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ActivityFunctions
{
    /// <summary>
    /// always exist when you need to trigger
    /// next activity in a group.
    /// </summary>
    [Guid("DABA9B4C-F4D6-E8C3-8F48-FFDD80C5C7B6")]
    public class SelectActivityInGroup : IActionable
    {
        [field: S] private PlayerActivityGroup GroupAccessor { get; set; }
        [field: S] private string CurrentActivityId { get; set; }
        [field: S] private string NextActivityId { get; set; }
        [field: S] private bool CurrentSuccess { get; set; }

        public void Invoke()
        {
            ActivityGroup group = GroupAccessor?.Value;

            if (group is not null)
            {
                if (!CurrentActivityId.IsNullOrEmpty())
                {
                    group.SetActivityResult(CurrentActivityId, CurrentSuccess);
                }

                if (!NextActivityId.IsNullOrEmpty())
                {
                    group.SetActivityActive(NextActivityId);
                }
            }
        }
    }
}