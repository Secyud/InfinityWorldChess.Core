using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.BattleDomain.BattleMapDomain
{
    public interface IBattleMapActionService
    {
        void OnHover(HexCell cell);

        void OnPress(HexCell cell);
        
        void OnTrig();

        void OnClear();
    }
}