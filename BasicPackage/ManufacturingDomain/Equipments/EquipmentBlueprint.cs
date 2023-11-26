using InfinityWorldChess.ItemTemplates;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ManufacturingDomain.Equipments
{
    public class EquipmentBlueprint : Item
    {
        private CustomEquipment _equipment;
        [field: S] public string EquipmentId { get; set; }

        public CustomEquipment InitEquipment()
        {
            _equipment ??= new CustomEquipment();

            U.Tm.LoadObjectFromResource(_equipment,EquipmentId);

            return _equipment;
        }
    }
}