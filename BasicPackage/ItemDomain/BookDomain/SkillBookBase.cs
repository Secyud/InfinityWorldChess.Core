#region

using System;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using InfinityWorldChess.BasicBundle.Items;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemDomain.BookDomain
{
    public abstract class SkillBookBase : ItemTemplate, IReadable
    {
        [field: S] public RoleItemFunctionBase Function { get; set; }

        public void Reading()
        {
            RoleGameContext roleContext = GameScope.Instance.Role;
            Role role = roleContext.MainOperationRole;
            Function?.Invoke(role);
            role.Item.Remove(this);
        }
    }
}