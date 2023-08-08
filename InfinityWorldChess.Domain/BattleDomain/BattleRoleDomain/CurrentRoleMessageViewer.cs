namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    public class CurrentRoleMessageViewer : RoleMessageViewerBase
    {
        private RoleRefreshItem _refreshItem;
        private RoleRefreshItem _stateRefreshItem;

        private void Awake()
        {
            _refreshItem ??=
                new RoleRefreshItem(nameof(CurrentRoleMessageViewer), Refresh);
            _stateRefreshItem ??=
                new RoleRefreshItem(nameof(CurrentRoleMessageViewer) + "State", RefreshState);
            _stateRefreshItem.Service.StateOnlyItems[_stateRefreshItem.Name] = _stateRefreshItem;
        }

        private void Refresh()
        {
            Bind(_refreshItem.Service.Role);
        }
    }
}