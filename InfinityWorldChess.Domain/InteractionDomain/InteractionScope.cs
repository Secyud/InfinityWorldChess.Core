
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.InteractionDomain
{
    [DependScope(typeof(GameScope))]
    public class InteractionScope:DependencyScope
    {
        private static  IMonoContainer<RoleInteractionComponent> _roleInteractionComponent;

        public static InteractionContext Context;
        
        public InteractionScope(DependencyManager dependencyProvider,IwcAb ab) : base(dependencyProvider)
        {
            _roleInteractionComponent ??= MonoContainer<RoleInteractionComponent>.Create(ab);
            _roleInteractionComponent.Create();
            Context = Get<InteractionContext>();
            Context.Component = _roleInteractionComponent.Value;
        }
        
        public void OnCreation(IInteractionUnit unit)
        {
            _roleInteractionComponent.Value.SetInteraction(unit);
        }

        public override void Dispose()
        {
            _roleInteractionComponent.Destroy();
            Context = null;
        }
    }
}