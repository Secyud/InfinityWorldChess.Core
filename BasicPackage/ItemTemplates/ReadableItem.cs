#region

using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemTemplates
{
    public class ReadableItem : Item, IReadable
    {
        [field: S] public IActionable<Role> Function { get; set; }

        public void Reading(Role role)
        {
            Function?.Invoke(role);
            role.Item.Remove(this,1);
        }

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            Function.TrySetContent(transform);
        }
    }
}