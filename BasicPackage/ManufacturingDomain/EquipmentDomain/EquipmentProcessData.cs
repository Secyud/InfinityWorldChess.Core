using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.ItemTemplates;

namespace InfinityWorldChess.ManufacturingDomain.EquipmentDomain
{
    public class EquipmentProcessData
    {
        public readonly Equipment Equipment = new();
        public readonly Dictionary<int, IBuff<Equipment>> BuffDict = new();
        public readonly float[] Property = new float[SharedConsts.EquipmentPropertyCount];
        public readonly float[] Shape = new float[SharedConsts.EquipmentPropertyCount];

        public Equipment FinishedEquipment()
        {
            for (int i = 0; i < SharedConsts.EquipmentPropertyCount; i++)
                Equipment.Property[i] = (int)Property[i];

            Equipment.Antique = (int)Shape.Sum() / 4;

            Equipment.Clear();
            
            foreach (IBuff<Equipment> buff in BuffDict.Values)
                Equipment.Install(buff);

            return Equipment;
        }
    }
}