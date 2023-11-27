using System;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.RoleAccessors
{
    public class ResourceRoleBuff : ResourceAccessor<IRoleBuff>
    {
        [field: S, TypeLimit(typeof(IRoleBuff))]
        public Guid ClassId { get; set; }

        protected override Guid TypeId => ClassId;
    }
}