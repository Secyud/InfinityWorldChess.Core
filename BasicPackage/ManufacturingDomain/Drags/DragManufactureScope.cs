using InfinityWorldChess.GameDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.ManufacturingDomain.Drags
{
    [Registry(DependScope = typeof(GameScope))]
    public class DragManufactureScope:DependencyScopeProvider
    {
        public static DragManufactureScope Instance { get; private set; }

        private readonly MonoContainer<DragManufacturePanel> _manufacturePanel;
        
        public DragManufactureContext Context => Get<DragManufactureContext>();
        
        public DragManufactureScope(IwcAssets assets)
        {
            _manufacturePanel = MonoContainer<DragManufacturePanel>.Create(assets);
        }
        
        public override void OnInitialize()
        {
            Instance = this;
            _manufacturePanel.Create();
        }

        public override void Dispose()
        {
            _manufacturePanel.Destroy();
            Instance = null;
        }
    }
}