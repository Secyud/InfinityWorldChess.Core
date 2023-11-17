using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleTemplates
{
    public class RoleWithIndex
    {
        [field: S(1)] public IObjectAccessor<Role> RoleAccessor { get; set; }
        [field: S(0)] public int  Index { get; set; }
    }
}