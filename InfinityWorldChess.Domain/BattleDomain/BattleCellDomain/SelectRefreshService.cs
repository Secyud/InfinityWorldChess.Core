using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.RefreshComponents;

namespace InfinityWorldChess.BattleDomain.BattleCellDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class SelectRefreshService : RefreshService<SelectRefreshService, SelectRefreshItem>
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
                RefreshState();
            }
        }

        public virtual Dictionary<string, SelectRefreshItem> StateOnlyItems { get; } = new();

        public void RefreshState()
        {
            foreach (SelectRefreshItem item in StateOnlyItems.Values)
                item.Refresh();
        }
    }
}