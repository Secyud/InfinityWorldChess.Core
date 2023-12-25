using System.Runtime.InteropServices;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.RoleAccessors
{
    [Guid("DB8AC4A6-CF7F-695B-BA8D-A8887DF3EAF4")]
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