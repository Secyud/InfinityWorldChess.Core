using System.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain.FlavorDomain;

namespace InfinityWorldChess.ManufacturingDomain.FoodDomain
{
    public class FoodProcessSelect:FlavorProcessSelect<
        Food,FoodProcessBase,FoodProcessData,FoodProcessSorters,FoodProcessFilters>
    {
        protected override IList<FoodProcessBase> GetSelectList()
        {
            return GameScope.Instance.Player.Role
                .GetProperty<FoodManufacturingProperty>()
                .LearnedProcesses;
        }

        protected override FlavorFlow<FoodProcessData, Food> GetFlow()
        {
            return Manufacture.Get<FoodFlow>();
        }
    }
}