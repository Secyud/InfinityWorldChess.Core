using System;
using System.Collections.Generic;
using InfinityWorldChess.BattleTemplates;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.MessageDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.LevelDomain
{
    public class BattleLevel : Battle, IBattleLevel
    {
        [field: S(17)] public List<IObjectAccessor<IItem>> Awards { get; } = new();

        [field: S(3)] public int Level { get; set; }

        public override void OnBattleFinished()
        {
            if (Victory)
            {
                foreach (IObjectAccessor<IItem> accessor in Awards)
                {
                    IItem item = accessor.Value;
                    GameScope.Instance.Player.Role.Item.Add(item);
                    if (item is IOverloadedItem overloadedItem)
                    {
                        MessageScope.Instance.AddMessage($"获得{item.Name}*{overloadedItem.Quantity}");
                    }
                    else
                    {
                        MessageScope.Instance.AddMessage($"获得{item.Name}");
                    }
                }

                Role.BasicProperty basic = GameScope.Instance.Player.Role.Basic;
                int level = Math.Max(0, Level - basic.Level + 0x100);
                basic.Level += level * 0x100;
            }
        }
    }
}