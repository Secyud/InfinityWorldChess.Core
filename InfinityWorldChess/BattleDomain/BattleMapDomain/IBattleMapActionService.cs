using InfinityWorldChess.BattleDomain.BattleCellDomain;

namespace InfinityWorldChess.BattleDomain.BattleMapDomain
{
    public interface IBattleMapActionService
    {
        void OnApply();
        
        void OnHover(BattleCell cell);

        void OnPress(BattleCell cell);
        
        void OnTrig();

        void OnClear();
    }
}