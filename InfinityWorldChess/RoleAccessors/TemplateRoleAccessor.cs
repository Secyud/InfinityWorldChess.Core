using System;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.RoleAccessors
{
    public class TemplateRoleAccessor:ResourceAccessor<RoleTemplate>,IObjectAccessor<Role>
    {
        [field:S] public Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
        public new Role Value => base.Value.GenerateRole();
    }
}