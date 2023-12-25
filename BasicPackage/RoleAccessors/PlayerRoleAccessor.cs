using System.Runtime.InteropServices;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleAccessors
{
    [Guid("D9BAFBCE-4A3F-D2D1-C962-17275A010CA2")]
    public class PlayerRoleAccessor:IObjectAccessor<Role>
    {
        public static PlayerRoleAccessor Instance { get; } = new();
        public Role Value => GameScope.Instance.Player.Role;
    }
}