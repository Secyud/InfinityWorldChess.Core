using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.RefreshComponents;

namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class RoleRefreshService: RefreshService<RoleRefreshService,RoleRefreshItem>
    {
        private BattleRole _role;

        public BattleRole Role
        {
            get => _role;
            internal set
            {
                if (_role == value)
                    return;
                _role = value;
                Refresh();
                RefreshState();
            }
        }
        
        public virtual Dictionary<string, RoleRefreshItem> StateOnlyItems { get; } = new();

        public void RefreshState()
        {
            foreach (RoleRefreshItem item in StateOnlyItems.Values)
                item.Refresh();
        }
    }
}