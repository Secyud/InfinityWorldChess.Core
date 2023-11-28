using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleAccessors
{
    public class PlayerRoleAccessor:IObjectAccessor<Role>
    {
        public static PlayerRoleAccessor Instance { get; } = new();
        public Role Value => GameScope.Instance.Player.Role;
    }
}