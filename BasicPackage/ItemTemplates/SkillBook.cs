#region

using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.RoleFunctions;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.ItemTemplates
{
    public class SkillBook : Item, IReadable
    {
        [field: S] public RoleItemFunctionBase Function { get; set; }

        public void Reading(Role role)
        {
            Function?.Invoke(role);
            role.Item.Remove(this);
        }
    }
}