namespace InfinityWorldChess.BattleDomain
{
    public interface IBattleMapActionService
    {
        void OnApply();
        
        void OnHover(BattleCell cell);

        void OnPress(BattleCell cell);
        
        void OnTrig();

        void OnClear();
        
        bool IsInterval { get; }
    }
}