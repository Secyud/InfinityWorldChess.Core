using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.ObserverComponents;

namespace InfinityWorldChess.ManufacturingDomain.Equipments
{
    [Registry(DependScope = typeof(EquipmentManufactureScope))]
    public class EquipmentManufactureContext:IRegistry
    {
        private EquipmentMaterial _selectedMaterial;

        public EquipmentMaterial SelectedMaterial
        {
            get => _selectedMaterial;
            set
            {
                if (_selectedMaterial == value)
                {
                    return;
                }

                _selectedMaterial = value;

                SelectedMaterialService.Refresh();
            }
        }

        public ObservedService SelectedMaterialService { get; } = new();
        
        private EquipmentBlueprint _selectedBlueprint;

        public EquipmentBlueprint SelectedBlueprint
        {
            get => _selectedBlueprint;
            set
            {
                if (_selectedBlueprint == value)
                {
                    return;
                }

                _selectedBlueprint = value;

                SelectedBlueprintService.Refresh();
            }
        }

        public ObservedService SelectedBlueprintService { get; } = new();
        
        private EquipmentProcessContainer[] _processes;

        public EquipmentProcessContainer[] Processes
        {
            get => _processes;
            set
            {
                if (_processes == value)
                {
                    return;
                }

                _processes = value;

                ProcessesService.Refresh();
            }
        }
        public ObservedService ProcessesService { get; } = new();

        public bool IsForging { get; internal set; } = false;

    }
}