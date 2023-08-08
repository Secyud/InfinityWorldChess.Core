using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain.FlavorDomain;
using InfinityWorldChess.ManufacturingDomain.MetalDomain;

namespace InfinityWorldChess.ManufacturingDomain.DragDomain
{
    public class DragProcessSelect:FlavorProcessSelect<
        Drag,DragProcessBase,DragProcessData,DragProcessSorters,DragProcessFilters>
    {
        protected override IList<DragProcessBase> GetSelectList()
        {
            return GameScope.Instance.Player.Role
                .GetProperty<DragManufacturingProperty>()
                .LearnedProcesses;
        }

        protected override FlavorFlow<DragProcessData, Drag> GetFlow()
        {
            return Manufacture.Get<DragFlow>();
        }
    }
}