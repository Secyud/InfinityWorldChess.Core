using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.ManufacturingDomain.FlavorDomain;

namespace InfinityWorldChess.ManufacturingDomain.FoodDomain
{
    public class FoodFlow : FlavorFlow<FoodProcessData, Food>
    {
        public override void OnInitialize()
        {
            SetFlowAction(64, RunProcess);
        }

        private void RunProcess(FoodProcessData data)
        {
            FoodFlavorSelect raw = Manufacture.Get<FoodFlavorSelect>();
            FoodProcessSelect processSelect = Manufacture.Get<FoodProcessSelect>();

            for (int i = 0; i < BasicConsts.FlavorTime; i++)
            {
                raw.Manufacturing(i, data);
                processSelect.Process(i, data);
                
                for (int j = 0; j < BasicConsts.FlavorCount; j++)
                    data.FlavorLevel[j] += data.FlavorLevel[j] * 0.01f;
            }
        }
    }
}