using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.ManufacturingFunctions;

namespace InfinityWorldChess.ManufacturingDomain.DragFunctions
{
    public class AddBattleUnitActionableToDrag:AddBattleUnitActionable,IActionable<CustomDrag>
    {
        public void Invoke(CustomDrag target)
        {
            Add(target);
        }
    }
}