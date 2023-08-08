using System;
using Secyud.Ugf.RefreshComponents;

namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    public class RoleRefreshItem:RefreshActionItem<RoleRefreshService,RoleRefreshItem>
    {
        public RoleRefreshItem(string name, Action action) : base(name, action)
        {
        }
    }
}