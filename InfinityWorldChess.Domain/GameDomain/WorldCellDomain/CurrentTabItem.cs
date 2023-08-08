using System;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class CurrentTabItem : TabActionItem<CurrentTabService, CurrentTabItem>
    {
        public CurrentTabItem(
             string name,
            GameObject gameObject, Action action)
            : base(name, gameObject, action)
        {
        }
    }
}