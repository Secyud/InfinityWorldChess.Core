using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain.FlavorDomain;

namespace InfinityWorldChess.ManufacturingDomain.DragDomain
{
    public class DragProcessData : FlavorProcessData, IHasFlavor
    {
        public readonly Drag Drag = new();
        public readonly Dictionary<int, IBuff<Drag>> BuffDict = new();

        public override IItem FinishedFlavor()
        {
            foreach (IBuff<Drag> buff in BuffDict.Values)
            {
                Drag.Install(buff);
            }

            for (int i = 0; i < BasicConsts.FlavorCount; i++)
            {
                Drag.FlavorLevel[i] = FlavorLevel[i];
            }

            return Drag;
        }
    }
}