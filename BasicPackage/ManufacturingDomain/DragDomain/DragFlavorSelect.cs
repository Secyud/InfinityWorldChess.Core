using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.ManufacturingDomain.FlavorDomain;

namespace InfinityWorldChess.ManufacturingDomain.DragDomain
{
    public class DragFlavorSelect:FlavorSelect<DragProcessData,Drag>
    {
        protected override FlavorFlow<DragProcessData, Drag> GetFlow()
        {
            return Manufacture.Get<DragFlow>();
        }
    }
}