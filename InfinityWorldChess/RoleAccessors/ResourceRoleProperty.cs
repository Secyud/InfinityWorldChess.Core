using System;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.RoleAccessors
{
    public class ResourceRoleProperty:ResourceAccessor<IRoleProperty>
    {
        [field: S,TypeLimit(typeof(IRoleProperty))]
        public Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
    }
}