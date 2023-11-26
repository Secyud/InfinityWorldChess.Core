using InfinityWorldChess.ItemTemplates;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ManufacturingDomain.Drags
{
    public class DragBlueprint : Item
    {
        private CustomDrag _drag;
        [field: S] public string DragId { get; set; }

        public CustomDrag InitDrag()
        {
            _drag ??= new CustomDrag();

            U.Tm.LoadObjectFromResource(_drag,DragId);

            return _drag;
        }
    }
}