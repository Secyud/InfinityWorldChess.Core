using System;
using System.Runtime.InteropServices;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.RoleAccessors
{
    [Guid("DAAA8AC5-94E8-A1C4-A595-34051A9BA370")]
    public class TemplateRoleAccessor:ResourceAccessor<RoleTemplate>,IObjectAccessor<Role>
    {
        [field:S,TypeLimit(typeof(RoleTemplate))] public Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
        public new Role Value => base.Value.GenerateRole();
    }
}