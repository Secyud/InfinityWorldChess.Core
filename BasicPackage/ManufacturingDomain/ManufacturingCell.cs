using InfinityWorldChess.GlobalDomain;

namespace InfinityWorldChess.ManufacturingDomain
{
    public class ManufacturingCell : ShownCell
    {
        public IManufacturingOperation Operation { get; set; }
        
        public void OnLeftClick()
        {
            Operation?.OnLeftClick(CellIndex);
        }

        public void OnHover()
        {
            Operation?.OnHover(CellIndex);
        }

        public void OnRightClick()
        {
            Operation?.OnRightClick(CellIndex);
        }
    }
}