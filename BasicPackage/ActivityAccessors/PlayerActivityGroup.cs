using System.Linq;
using System.Runtime.InteropServices;
using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.GameDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ActivityAccessors
{
    [Guid("C7AAAC34-DD42-22CB-3DAC-C953FADC256C")]
    public class PlayerActivityGroup: IObjectAccessor<ActivityGroup>
    {
        [field:S] private string GroupId { get; set; }

        public virtual ActivityGroup Value
        {
            get
            {
                ActivityGroup group = GameScope.Instance.Player.Activity
                    .FirstOrDefault(u => u.ResourceId == GroupId);
                return group;
            }
        }
    }
}