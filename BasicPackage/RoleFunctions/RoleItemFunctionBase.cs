﻿using System;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.RoleFunctions
{
    public abstract class RoleItemFunctionBase : ITrigger<Role>
    {
        public abstract void Invoke(Role role);
    }

    public abstract class RoleItemFunctionBase<TItem> : RoleItemFunctionBase
    {
        [field: S] public string Name { get; set; }
        [field: S] public Guid ClassId { get; set; }

        public override void Invoke(Role role)
        {
            if (U.Tm.ConstructFromResource(ClassId, Name)
                is not TItem item)
            {
                U.LogError($"unable to read resource {Name};");
                return;
            }

            Invoke(role, item);
        }

        protected abstract void Invoke(Role role, TItem item);
    }
}