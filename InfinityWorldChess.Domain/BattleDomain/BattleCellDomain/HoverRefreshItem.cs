using System;
using Secyud.Ugf.RefreshComponents;

namespace InfinityWorldChess.BattleDomain.BattleCellDomain
{
    public class HoverRefreshItem:RefreshActionItem<HoverRefreshService,HoverRefreshItem> 
    {
        public HoverRefreshItem(string name, Action action) : base(name, action)
        {
        }
    }
}