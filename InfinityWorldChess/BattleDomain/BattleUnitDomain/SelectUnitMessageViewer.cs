using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.BattleDomain
{
    public class SelectUnitMessageViewer : UnitMessageViewerBase
    {
        private BattleContext _context;

        private void Awake()
        {
            _context = BattleScope.Instance.Context;
            _context.SelectedCellService.AddObserverObject(
                nameof(SelectUnitMessageViewer), Refresh, gameObject);
            _context.StateService.AddObserverObject(
                nameof(SelectUnitMessageViewer), Refresh, gameObject);
            Refresh();
        }

        private void Refresh()
        {
            HexCell cell = BattleScope.Instance.Context.SelectedCell;
            BattleUnit unit = null;

            if (cell is not null && cell.Unit)
            {
                unit = cell.Unit as BattleUnit;
            }

            Bind(unit);

            RefreshState();
        }
    }
}