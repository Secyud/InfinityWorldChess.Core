using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.ManufacturingFunctions;

namespace InfinityWorldChess.ManufacturingDomain.DragFunctions
{
    public class ChangeDragProperty:ChangeAttachProperty,IActionable<CustomDrag>
    {
        public void Invoke(CustomDrag target)
        {
            Change(target);
        }
    }
}