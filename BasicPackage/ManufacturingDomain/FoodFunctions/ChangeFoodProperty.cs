using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.ManufacturingFunctions;

namespace InfinityWorldChess.ManufacturingDomain.FoodFunctions
{
    public class ChangeFoodProperty : ChangeAttachProperty,IActionable<CustomFood>
    {
        public void Invoke(CustomFood target)
        {
            Change(target);
        }
    }
}