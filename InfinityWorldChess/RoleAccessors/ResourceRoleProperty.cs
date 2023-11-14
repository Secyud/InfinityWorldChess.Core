using System;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.RoleAccessors
{
    public class ResourceRoleProperty:ResourceAccessor<RoleProperty>
    {
        [field: S,TypeLimit(typeof(RoleProperty))]
        public Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
    }
}