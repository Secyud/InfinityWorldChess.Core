using System;
using System.Runtime.InteropServices;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.Ugf
{
    public abstract class ResourceAccessor<TResource> : IObjectAccessor<TResource>
        where TResource : class
    {
        [field: S] public string ResourceId { get; set; }

        protected abstract Guid TypeId { get; }

        public virtual TResource Value =>
            U.Tm.ReadObjectFromResource(TypeId, ResourceId) as TResource;
    }
}