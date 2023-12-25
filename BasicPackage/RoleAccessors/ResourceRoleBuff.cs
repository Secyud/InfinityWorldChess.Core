using System;
using System.Runtime.InteropServices;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.RoleAccessors
{
    [Guid("50A39633-BFCD-ABA5-D8ED-753C2BAA79DC")]
    public class ResourceRoleBuff : ResourceAccessor<IRoleBuff>
    {
        [field: S, TypeLimit(typeof(IRoleBuff))]
        public Guid ClassId { get; set; }

        protected override Guid TypeId => ClassId;
    }
}