using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ManufacturingDomain.Components;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.LayoutComponents;
using Secyud.Ugf.TableComponents.FilterComponents;
using Secyud.Ugf.TableComponents.SorterComponents;
using UnityEngine;
namespace InfinityWorldChess.ManufacturingDomain.FlavorDomain
{
    public abstract class FlavorProcessSelect<TFlavor,TProcess,TProcessData,TProcessSorters,TProcessFilters>:
        Select<TProcess, TProcessSorters, TProcessFilters> 
        where TProcess : FlavorProcessBase<TProcessData>
        where TProcessSorters : SorterRegeditBase<TProcess> 
        where TProcessFilters : FilterRegeditBase<TProcess>
        where TProcessData : FlavorProcessData, new()
        where TFlavor : IItem
    {
        [SerializeField] private LayoutGroupTrigger Layout;
        [SerializeField] private ManufacturingCell ManufacturingCellPrefab;
        public TProcess[] Occupied { get; private set; }
        public ManufacturingCell[] Cells { get; private set; }

        protected virtual void Awake()
        {
            ClearBoard();
        }

        private void CellRightClickAction(ManufacturingCell cell)
        {
            TProcess process = Occupied[cell.Index];

            if (process is not null)
            {
                Occupied[cell.Index] = null;
                Cells[cell.Index].Image.color = Color.white;
            }
        }

        private void CellClickAction(ManufacturingCell cell)
        {
            CellRightClickAction(cell);

            if (SelectedItem is null)
                return;

            Occupied[cell.Index] = SelectedItem;
            Cells[cell.Index].Image.color = SelectedItem.Color;
        }

        private void CellHoverAction(ManufacturingCell cell)
        {
            TProcess process = Occupied[cell.Index];

            process?.CreateAutoCloseFloatingOnMouse();
        }

        protected abstract FlavorFlow<TProcessData, TFlavor> GetFlow();

        public override void OnInitialize()
        {
            FlavorFlow<TProcessData, TFlavor> flow = GetFlow();
            flow.SetClearAction(64, ClearBoard);
        }

        private void ClearBoard()
        {
            Layout.PrepareLayout();
            RectTransform content = Layout.PrepareLayout();
            Occupied = new TProcess[BasicConsts.FlavorTime];
            Cells = new ManufacturingCell[BasicConsts.FlavorTime];
            for (int i = 0; i < BasicConsts.FlavorTime; i++)
            {
                ManufacturingCell cell = ManufacturingCellPrefab.Instantiate(content);
                Cells[i] = cell;
                cell.Index = i;
                cell.ClickAction = CellClickAction;
                cell.HoverAction = CellHoverAction;
                cell.RightClickAction = CellRightClickAction;
            }
        }
        
        
        public void Process(int index,TProcessData data)
        {
            if (Occupied[index] is not null)
                Occupied[index].Process(Manufacture,data);
        }
    }
}