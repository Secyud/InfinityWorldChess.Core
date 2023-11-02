using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DataOperation.Accessors.RoleAccessors
{
    public class RoleFromGameById:IObjectAccessor<Role>
    {
        [field:S] public int RoleId { get; set; }
        
        public Role Value
        {
            get
            {
                GameScope.Instance.Role.TryGetValue(RoleId, out Role role);
                return role;
            }
        }
    }
}