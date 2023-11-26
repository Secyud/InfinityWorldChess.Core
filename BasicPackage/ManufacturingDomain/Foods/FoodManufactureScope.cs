using InfinityWorldChess.GameDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.ManufacturingDomain.Foods
{
    [Registry(DependScope = typeof(GameScope))]
    public class FoodManufactureScope:DependencyScopeProvider
    {
        public static FoodManufactureScope Instance { get; private set; }

        private readonly MonoContainer<FoodManufacturePanel> _manufacturePanel;
        
        public FoodManufactureContext Context => Get<FoodManufactureContext>();
        
        public FoodManufactureScope(IwcAssets assets)
        {
            _manufacturePanel = MonoContainer<FoodManufacturePanel>.Create(assets);
        }
        
        public override void OnInitialize()
        {
            Instance = this;
            _manufacturePanel.Create();
        }

        public override void Dispose()
        {
            Instance = null;
            _manufacturePanel.Destroy();
        }
    }
}