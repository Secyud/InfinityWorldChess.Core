#region

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
        [field: S(ID = 1)] public string SkillName { get; set; }

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddParagraph($"阅读可以学会{SkillName}");
        }

        public void Reading()
        {
            RoleGameContext roleContext = GameScope.Instance.Role;
            Role role = roleContext.MainOperationRole;
            Reading(role);
            role.Item.Remove(this);
        }

        protected abstract void Reading(Role role);
    }
}