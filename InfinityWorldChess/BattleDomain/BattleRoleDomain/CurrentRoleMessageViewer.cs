namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    public class CurrentRoleMessageViewer : RoleMessageViewerBase
    {
        private RoleObservedService _roleObservedService;
        private void Awake()
        {
            _roleObservedService = BattleScope.Instance.Get<RoleObservedService>();
            _roleObservedService.AddObserverObject(nameof(CurrentRoleMessageViewer), Refresh,gameObject);
            _roleObservedService.State.AddObserverObject(nameof(CurrentRoleMessageViewer), RefreshState,gameObject);
        }

        private void Refresh()
        {
            Bind(_roleObservedService.Role);
        }
    }
}