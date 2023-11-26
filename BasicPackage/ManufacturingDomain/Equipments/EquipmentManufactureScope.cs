using InfinityWorldChess.GameDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.ManufacturingDomain.Equipments
{
    [Registry(DependScope = typeof(GameScope))]
    public class EquipmentManufactureScope:DependencyScopeProvider
    {
        public static EquipmentManufactureScope Instance { get; private set; }

        private readonly MonoContainer<EquipmentManufacturePanel> _manufacturePanel;
        
        public EquipmentManufactureContext Context => Get<EquipmentManufactureContext>();
        
        public EquipmentManufactureScope(IwcAssets assets)
        {
            _manufacturePanel = MonoContainer<EquipmentManufacturePanel>.Create(assets);
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