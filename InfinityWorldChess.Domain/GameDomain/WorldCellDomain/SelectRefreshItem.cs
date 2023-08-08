using System;
using Secyud.Ugf.RefreshComponents;

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class SelectRefreshItem:RefreshActionItem<SelectRefreshService,SelectRefreshItem>
    {
        public SelectRefreshItem(string name, Action action) 
            : base( name, action)
        {
        }
    }
}