using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.DataOperation.Accessors.RoleAccessors
{
    public class RoleDirectly:IObjectAccessor<Role>
    {
        public Role Value { get; set; }
    }
}