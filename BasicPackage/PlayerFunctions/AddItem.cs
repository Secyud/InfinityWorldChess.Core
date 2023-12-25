using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.MessageDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.PlayerFunctions
{
    public class AddItem:AccessorWithTemplate<Item>, IActionable ,IHasContent
    {
        public void Invoke()
        {
            Item item = Accessor.Value;
            if (item is IOverloadedItem overloadedItem)
            {
                MessageScope.Instance.AddMessage($"获得{item.Name}*{overloadedItem.Quantity}");
            }
            else
            {
                MessageScope.Instance.AddMessage($"获得{item.Name}");
            }
            GameScope.Instance.Player.Role.Item.Add(item);
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph( $"可获得{Template.Name}。");
            base.SetContent(transform);
        }
    }
}