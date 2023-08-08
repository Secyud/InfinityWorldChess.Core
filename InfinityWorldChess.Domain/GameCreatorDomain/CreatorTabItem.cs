using System;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorTabItem : TabActionItem<CreatorTabService, CreatorTabItem>
    {
        public CreatorTabItem(string name, GameObject gameObject, Action action = null) 
            : base(name, gameObject, action)
        {
        }
    }
}