using InfinityWorldChess.ItemDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddItem:RoleItemFunctionBase<IItem>,IHasDescription
    {
        public  string Description => $"可获得{Name}。";

        protected override void Invoke(Role role, IItem item)
        {
            role.Item.Add(item);
        }
    }
}