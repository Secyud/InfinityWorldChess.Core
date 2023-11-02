using System.Linq;
using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.GameDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DataOperation.Accessors.ActivityAccessors
{
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