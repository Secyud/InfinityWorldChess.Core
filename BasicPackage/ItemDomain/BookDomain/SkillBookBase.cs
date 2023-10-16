#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.ItemDomain.BookDomain
{
    public abstract class SkillBookBase : Item, IReadable
    {
        [field: S] public RoleItemFunctionBase Function { get; set; }

        public void Reading(Role role)
        {
            Function?.Invoke(role);
            role.Item.Remove(this);
        }
    }
}