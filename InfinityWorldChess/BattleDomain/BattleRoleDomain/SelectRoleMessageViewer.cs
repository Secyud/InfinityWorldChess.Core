using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.BattleDomain
{
    public class SelectRoleMessageViewer : RoleMessageViewerBase
    {
        private BattleContext _context;

        private void Awake()
        {
            _context = BattleScope.Instance.Context;
            _context.SelectedCellService.AddObserverObject(
                nameof(SelectRoleMessageViewer), Refresh, gameObject);
            _context.StateService.AddObserverObject(
                nameof(SelectRoleMessageViewer), Refresh, gameObject);
            Refresh();
        }

        private void Refresh()
        {
            HexCell cell = BattleScope.Instance.Context.SelectedCell;
            BattleRole chess = null;

            if (cell is not null  && cell.Unit)
            {
                chess = cell.Unit as BattleRole;
            }

            Bind(chess);
            
            RefreshState();
        }
    }
}