using InfinityWorldChess.ManufacturingDomain.Drags;
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
        
        private DragMaterialContainer[] _dragMaterials;

        public DragMaterialContainer[] DragMaterials
        {
            get => _dragMaterials;
            set
            {
                if (_dragMaterials == value)
                {
                    return;
                }

                _dragMaterials = value;

                ProcessesService.Refresh();
            }
        }
        public ObservedService ProcessesService { get; } = new();

        public bool IsForging { get; internal set; } = false;

    }
}