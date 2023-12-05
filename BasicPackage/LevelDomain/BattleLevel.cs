using System.Collections.Generic;
using InfinityWorldChess.BattleTemplates;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.MessageDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.LevelDomain
{
    public class BattleLevel:Battle,IBattleLevel
    {
        [field: S(3)] public List<IObjectAccessor<IItem>> Awards { get; } = new();
        
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
            }
        }
    }
}