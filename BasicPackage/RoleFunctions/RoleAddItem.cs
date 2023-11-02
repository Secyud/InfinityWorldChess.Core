using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleFunctions
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