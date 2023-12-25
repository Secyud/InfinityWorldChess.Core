using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.ManufacturingFunctions;

namespace InfinityWorldChess.ManufacturingDomain.DragFunctions
{
    public class AddBattleUnitActionableToFood:AddBattleUnitActionable,IActionable<CustomFood>
    {
        public void Invoke(CustomFood target)
        {
            Add(target);
        }
    }
}