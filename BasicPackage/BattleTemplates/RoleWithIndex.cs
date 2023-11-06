using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleTemplates
{
    public class RoleWithIndex
    {
        [field: S] public IObjectAccessor<Role> RoleAccessor { get; set; }
        [field: S] public int  Index { get; set; }
    }
}