using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain.FlavorDomain;

namespace InfinityWorldChess.ManufacturingDomain.FoodDomain
{
    public class FoodProcessData:FlavorProcessData,IHasMouthfeel
    {
        public readonly Food Food = new();
        public readonly Dictionary<int, IBuff<Food>> BuffDict = new();

        public float[] MouthFeelLevel { get; } = new float[BasicConsts.MouthFeelCount];

        public override IItem FinishedFlavor()
        {
            foreach (IBuff<Food> buff in BuffDict.Values)
            {
                Food.Install(buff);
            }

            for (int i = 0; i < BasicConsts.FlavorCount; i++)
            {
                Food.FlavorLevel[i] = FlavorLevel[i];
            }

            return Food;
        }
    }
}