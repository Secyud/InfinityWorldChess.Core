using System;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ItemAccessor
{
    public class ResourceEquipment:ResourceAccessor<IEquipment>
    {
        
        [field: S,TypeLimit(typeof(IEquipment))]
        public Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
    }
}