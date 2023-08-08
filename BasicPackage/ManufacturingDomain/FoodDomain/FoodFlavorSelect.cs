using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain.FlavorDomain;

namespace InfinityWorldChess.ManufacturingDomain.FoodDomain
{
    public abstract class FoodFlavorSelect :FlavorSelect<FoodProcessData,Food>
    {
        protected override FlavorFlow<FoodProcessData, Food> GetFlow()
        {
            return Manufacture.Get<FoodFlow>();
        }
    }
}