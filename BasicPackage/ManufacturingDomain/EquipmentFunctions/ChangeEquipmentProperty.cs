using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.ManufacturingFunctions;

namespace InfinityWorldChess.ManufacturingDomain.EquipmentFunctions
{
    public class ChangeEquipmentProperty : ChangeAttachProperty, IActionable<CustomEquipment>
    {
        public void Invoke(CustomEquipment target)
        {
            Change(target);
        }
    }
}