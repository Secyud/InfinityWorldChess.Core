#region

using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.WoodDomain
{
    public abstract class
        WoodEquipmentProcess : EquipmentProcessBase
    {
        public short Position { get; set; }

        [field: S(ID = 3, DataType = DataType.Initialed)]

        public byte RangeX { get; protected set; }

        [field: S(ID = 4, DataType = DataType.Initialed)]
        public byte RangeY { get; protected set; }
    }
}