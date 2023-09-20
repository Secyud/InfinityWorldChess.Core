using InfinityWorldChess.BattleDomain.BattleCellDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    public class SelectRoleMessageViewer : RoleMessageViewerBase
    {
        private SelectObservedService _select;
        private StateObservedService _state;
        private void Awake()
        {
            _select = U.Get<SelectObservedService>();
            _select.AddObserverObject(nameof(SelectRoleMessageViewer), Refresh,gameObject);
            Refresh();
        }

        private void Refresh()
        {
            BattleCell cell = _select.SelectedCell;
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