using System;
using Secyud.Ugf.RefreshComponents;

namespace InfinityWorldChess.BattleDomain.BattleCellDomain
{
    public class SelectRefreshItem:RefreshActionItem<SelectRefreshService,SelectRefreshItem>
    {
        public SelectRefreshItem(string name, Action action) : base(name, action)
        {
        }
    }
}