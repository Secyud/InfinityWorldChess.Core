using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.ObserverComponents;

namespace InfinityWorldChess.BattleDomain.BattleCellDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class SelectObservedService : ObservedService
    {
        public  ObservedService State { get; } = new();
        
        private BattleCell _selectedCell;

        public BattleCell SelectedCell
        {
            get => _selectedCell;
            set
            {
                if (_selectedCell == value)
                    return;
                _selectedCell = value;
                Refresh();
                State.Refresh();
            }
        }

        public void RefreshState()
        {
            State.Refresh();
        }
    }
}