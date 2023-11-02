using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.InteractionDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class InteractionScope : DependencyScopeProvider
    {
        private readonly MonoContainer<InteractionPanel> _interactionPanel;
        
        public DialogueService DialogueService => Get<DialogueService>();

        public static InteractionScope Instance { get; private set; }
        
        public InteractionScope(IwcAssets iwcAssets)
        {
            _interactionPanel = MonoContainer<InteractionPanel>.Create(iwcAssets);
        }

        public override void OnInitialize()
        {
            Instance = this;
        }

        public void SetSelectRole(Role role)
        {
            if (role is null)
            {
                _interactionPanel.Destroy();
            }
            else
            {
                if (!_interactionPanel.Value)
                    _interactionPanel.Create();
            
                InteractionTabService service = Get<InteractionTabService>();

                service.InteractionRole = role;
            }
        }
    }
}