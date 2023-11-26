using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.ObserverComponents;

namespace InfinityWorldChess.ManufacturingDomain.Drags
{
    [Registry(DependScope = typeof(DragManufactureScope))]
    public class DragManufactureContext:IRegistry
    {
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
        
        private DragMaterialContainer[] _materials;

        public DragMaterialContainer[] Materials
        {
            get => _materials;
            set
            {
                if (_materials == value)
                {
                    return;
                }

                _materials = value;

                ProcessesService.Refresh();
            }
        }
        public ObservedService ProcessesService { get; } = new();

        public bool IsForging { get; internal set; } = false;
    }
}