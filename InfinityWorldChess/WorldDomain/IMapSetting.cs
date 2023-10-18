namespace InfinityWorldChess.WorldDomain
{
    public interface IMapSetting
    {
        int PlayerInitialIndex { get; }
        void SetMap();
    }
}