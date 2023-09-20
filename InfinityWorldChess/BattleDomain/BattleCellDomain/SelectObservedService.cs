using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.ObserverComponents;

namespace InfinityWorldChess.BattleDomain.BattleCellDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class SelectObservedService : ObservedService
    {
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
            }
        }
    }
}