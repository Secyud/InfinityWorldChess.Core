using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain.FlavorDomain;

namespace InfinityWorldChess.ManufacturingDomain.DragDomain
{
    public class DragFlow : FlavorFlow<DragProcessData, Drag>
    {
        public override void OnInitialize()
        {
            SetFlowAction(64, RunProcess);
        }

        private void RunProcess(DragProcessData data)
        {
            DragFlavorSelect raw = Manufacture.Get<DragFlavorSelect>();
            DragProcessSelect processSelect = Manufacture.Get<DragProcessSelect>();

            for (int i = 0; i < BasicConsts.FlavorTime; i++)
            {
                raw.Manufacturing(i, data);
                processSelect.Process(i, data);
                
                for (int j = 0; j < BasicConsts.FlavorCount; j++)
                for (int k = 0; k < BasicConsts.FlavorCount; k++)
                    data.FlavorLevel[j] += data.DragProperty[j, k] * 0.01f;
            }
        }
    }
}