using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;

namespace InfinityWorldChess.ManufacturingDomain.FlavorDomain
{
    public abstract class FlavorProcessData:IHasFlavor
    {
        public abstract IItem FinishedFlavor();
        public float[] FlavorLevel { get; } = new float[BasicConsts.FlavorCount];
        public float[,] DragProperty { get; } = new float[BasicConsts.FlavorCount, BasicConsts.FlavorCount];

    }
}