using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleAccessors
{
    public class RoleDirectly:IObjectAccessor<Role>
    {
        public Role Value { get; set; }
    }
}