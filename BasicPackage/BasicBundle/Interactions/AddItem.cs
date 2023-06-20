using InfinityWorldChess.BasicBundle.Items;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.Resource;

namespace InfinityWorldChess.BasicBundle.Interactions
{
    public class AddItem : InteractionAction
    {
        private ItemTemplate _item;

        public override void Invoke()
        {
            if (_item is not null)
                GameScope.PlayerGameContext.Role.Item.Add(_item);
        }

        public override void SetAction(params string[] message)
        {
            _item = message[0].CreateAndInit<ItemTemplate>();
        }
    }
}