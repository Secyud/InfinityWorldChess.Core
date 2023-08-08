using InfinityWorldChess.BattleDomain.BattleCellDomain;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    public class SelectRoleMessageViewer : RoleMessageViewerBase
    {
        private SelectRefreshItem _refreshItem;
        private SelectRefreshItem _stateRefreshItem;

        private void Awake()
        {
            _refreshItem ??=
                new SelectRefreshItem(nameof(SelectRoleMessageViewer), Refresh);
            _stateRefreshItem ??=
                new SelectRefreshItem(nameof(SelectRoleMessageViewer) + "State", RefreshState);
            _stateRefreshItem.Service.StateOnlyItems[_stateRefreshItem.Name] = _stateRefreshItem;
        }

        private void Refresh()
        {
            BattleCell cell = _refreshItem.Service.SelectedCell;
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