using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.ObserverComponents;

namespace InfinityWorldChess.ManufacturingDomain.Foods
{
    [Registry(DependScope = typeof(FoodManufactureScope))]
    public class FoodManufactureContext:IRegistry
    {
        private FoodMaterial _selectedMaterial;

        public FoodMaterial SelectedMaterial
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
        
        private FoodBlueprint _selectedBlueprint;

        public FoodBlueprint SelectedBlueprint
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
        
        private FoodProcessContainer[] _processes;

        public FoodProcessContainer[] Processes
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