using System.Runtime.InteropServices;
using InfinityWorldChess.ActivityDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ActivityAccessors
{
    [Guid("70362C57-FF9D-0B1F-0D67-1FC47E5DBACE")]
    public class ResourceActivityGroup: IObjectAccessor<ActivityGroup>
    {
        [field:S] private string GroupId { get; set; }

        public virtual ActivityGroup Value => 
            U.Tm.ReadObjectFromResource<ActivityGroup>(GroupId);
    }
}