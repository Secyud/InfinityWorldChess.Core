using System;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ItemAccessor
{
    public class ResourceItem : ResourceAccessor<IItem>
    {
        [field: S, TypeLimit(typeof(IItem))] public Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
    }
}