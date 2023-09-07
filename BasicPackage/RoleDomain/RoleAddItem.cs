using InfinityWorldChess.ItemDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddItem:RoleItemFunctionBase
    {
        public override string Description => $"可获得{Name}。";

        public override bool Invoke(Role role)
        {
            if (U.Tm.ConstructFromResource(ClassId, Name) is not IItem item)
                return false;
            role.Item.Add(item);
            return true;
        }
    }
}