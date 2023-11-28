using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleTemplates
{
    [ID("76334757-99CD-43E0-0EF7-E777237B2DA0")]
    public class RoleWithPosition
    {
        [field: S(1)] public IObjectAccessor<Role> RoleAccessor { get; set; }
        [field: S(0)] public int  PositionX { get; set; }
        [field: S(0)] public int  PositionZ { get; set; }
    }
}