using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.TabComponents;

namespace InfinityWorldChess.InteractionDomain
{
    [Registry(DependScope = typeof(InteractionScope))]
    public class InteractionTabService: TabService
    {
        private Role _interactionRole;

        public Role InteractionRole
        {
            get => _interactionRole;
            set
            {
                _interactionRole = value;
                if (_interactionRole is not null)
                    RefreshCurrentTab();
            }
        }
    }
}