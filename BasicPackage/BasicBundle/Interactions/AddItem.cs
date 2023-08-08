using InfinityWorldChess.BasicBundle.Items;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.Interactions
{
    public class AddItem : InteractionAction
    {
        [field: S(ID = 0)]public string ItemName;

        public override void Invoke()
        {
            if (ItemName is null) return;
            ItemTemplate item = DataObject.Create<ItemTemplate>(ItemName);
            GameScope.Instance.Player.Role.Item.Add(item);
        }
    }
}