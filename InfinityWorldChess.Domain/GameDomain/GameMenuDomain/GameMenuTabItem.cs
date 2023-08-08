using System;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class GameMenuTabItem : TabActionItem<GameMenuTabService, GameMenuTabItem>
    {
        public GameMenuTabItem(string name, GameObject gameObject, Action action)
            : base(name, gameObject, action)
        {
        }
    }
}