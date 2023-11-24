using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.RoleFunctions
{
    public class RoleAddItem:AccessorWithTemplate<IItem>,
        IActionable<Role> ,IHasContent
    {
        public void Invoke(Role target)
        {
            target.Item.Add(Accessor.Value);
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph( $"可获得{Template.Name}。");
            base.SetContent(transform);
        }
    }
}