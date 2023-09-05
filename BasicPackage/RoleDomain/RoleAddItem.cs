using InfinityWorldChess.BasicBundle.Items;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddItem:RoleItemFunctionBase
    {
        public override string Description => $"可获得{Name}。";

        public override void Invoke(Role role)
        {
            if (U.Tm.Create(ClassId, Name) is IItem item)
            {
                role.Item.Add(item);
            }
        }
    }
}