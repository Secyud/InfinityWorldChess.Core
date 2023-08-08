using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.InteractionDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class InteractionScope : DependencyScopeProvider
    {
        private static IMonoContainer<RoleInteractionComponent> _roleInteractionComponent;

        public static InteractionScope Instance { get; private set; }
        
        public Role LeftRole { get; set; }
        public Role RightRole { get; set; }
        public RoleInteractionComponent Component => _roleInteractionComponent.Value;

        public InteractionScope(IwcAb ab)
        {
            _roleInteractionComponent ??= MonoContainer<RoleInteractionComponent>.Create(ab);
            _roleInteractionComponent.Create();
            Instance = this;
        }

        public void OnCreation(IInteractionUnit unit)
        {
            _roleInteractionComponent.Value.SetInteraction(unit);
        }

        public override void Dispose()
        {
            _roleInteractionComponent.Destroy();
            Instance = null;
        }
    }
}