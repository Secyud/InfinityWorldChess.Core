using InfinityWorldChess.BattleDomain.BattleCellDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    public class SelectRoleMessageViewer : RoleMessageViewerBase
    {
        private SelectObservedService _selectObservedService;
        private void Awake()
        {
            _selectObservedService = U.Get<SelectObservedService>();
            _selectObservedService.AddObserverObject(nameof(SelectRoleMessageViewer), Refresh,gameObject);
            _selectObservedService.State.AddObserverObject(nameof(SelectRoleMessageViewer), RefreshState,gameObject);
            Refresh();
        }

        private void Refresh()
        {
            BattleCell cell = _selectObservedService.SelectedCell;
            BattleRole chess = null;
            
            if (cell is not null && cell.Cell.Unit)
            {
                HexUnit unit = cell.Cell.Unit;
                chess = BattleScope.Instance.GetChess(unit);
            }

            Bind(chess);
        }
    }
}