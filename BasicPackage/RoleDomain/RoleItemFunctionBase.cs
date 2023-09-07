using System;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.RoleDomain
{
    public abstract class RoleItemFunctionBase
    {
        [field: S] public string Name { get; set; }
        [field: S] public Guid ClassId { get; set; }

        public abstract string Description { get; }
        public abstract bool Invoke(Role role);
    }
}