using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.ObserverComponents;

namespace InfinityWorldChess.ManufacturingDomain.Drags
{
    [Registry(DependScope = typeof(DragManufactureScope))]
    public class DragManufactureContext:IRegistry
    {
        private DragMaterial _selectedMaterial;

        public DragMaterial SelectedMaterial
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
        
        private DragBlueprint _selectedBlueprint;

        public DragBlueprint SelectedBlueprint
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
        
        private DragProcessContainer[] _processes;

        public DragProcessContainer[] Processes
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