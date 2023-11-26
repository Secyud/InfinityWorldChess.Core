namespace InfinityWorldChess.ManufacturingDomain
{
    public interface IManufacturingOperation
    {
        void OnLeftClick(int index);

        void OnHover(int index);

        void OnRightClick(int index);
    }
}