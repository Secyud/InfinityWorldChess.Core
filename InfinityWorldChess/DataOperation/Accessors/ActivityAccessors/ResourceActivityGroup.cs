using InfinityWorldChess.ActivityDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DataOperation.Accessors.ActivityAccessors
{
    public class ResourceActivityGroup: IObjectAccessor<ActivityGroup>
    {
        [field:S] private string GroupId { get; set; }

        public virtual ActivityGroup Value => 
            U.Tm.ConstructFromResource<ActivityGroup>(GroupId);
    }
}