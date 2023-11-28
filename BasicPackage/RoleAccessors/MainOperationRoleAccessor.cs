using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleAccessors
{
    public class MainOperationRoleAccessor:IObjectAccessor<Role>
    {
        public static MainOperationRoleAccessor Instance { get; } = new();
        
        public Role Value => GameScope.Instance.Role.MainOperationRole;
    }
}