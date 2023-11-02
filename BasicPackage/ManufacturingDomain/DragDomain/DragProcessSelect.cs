using System.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.ManufacturingDomain.FlavorDomain;

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