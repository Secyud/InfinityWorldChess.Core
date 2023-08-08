using System.Collections.Generic;
using InfinityWorldChess.ManufacturingDomain.Components;

namespace InfinityWorldChess.ManufacturingDomain.EquipmentDomain
{
    public abstract class EquipmentProcessSelect
        : Select<EquipmentProcessBase, EquipmentProcessSorters, EquipmentProcessFilters>
    {
        protected readonly List<EquipmentProcessBase> Processes = new();
    }
}