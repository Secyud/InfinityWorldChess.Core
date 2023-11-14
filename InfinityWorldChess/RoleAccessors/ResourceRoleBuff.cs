using System;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.RoleAccessors
{
    public class ResourceRoleBuff : ResourceAccessor<IBuff<Role>>
    {
        [field: S, TypeLimit(typeof(IBuff<Role>))]
        public Guid ClassId { get; set; }

        protected override Guid TypeId => ClassId;
    }
}