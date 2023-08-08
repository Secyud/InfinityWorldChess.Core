using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.StoneDomain
{
    public class StoneProcessFunction : EquipmentProcessSelect
    {
        [SerializeField] private LayoutGroupTrigger Layout;
        [SerializeField] private ManufacturingCell ManufacturingCellPrefab;

        public StoneEquipmentProcess[] Occupied { get; private set; }
        public ManufacturingCell[] Cells { get; private set; }

        private void CellRightClickAction(ManufacturingCell cell)
        {
            StoneEquipmentProcess process = Occupied[cell.Index];

            if (process is not null)
            {
                Occupied[cell.Index] = null;
                Cells[cell.Index].Image.color = Color.white;

                Processes.Remove(process);
            }
        }

        private void CellClickAction(ManufacturingCell cell)
        {
            CellRightClickAction(cell);
            
            if (SelectedItem is not StoneEquipmentProcess process ||
                process.RangeStart > cell.Index ||
                process.RangeEnd <= cell.Index)
            {
                "超出范围".CreateTipFloatingOnCenter();
                return;
            }

            process.Position = (byte)cell.Index;
            Occupied[cell.Index] = process;
            Cells[cell.Index].Image.color = process.Color;
            Processes.Add(process);
        }

        private void CellHoverAction(ManufacturingCell cell)
        {
            StoneEquipmentProcess process = Occupied[cell.Index];

            process?.CreateAutoCloseFloatingOnMouse();
        }

        public void ReselectRaw(StoneEquipmentRaw item)
        {
            RectTransform content = Layout.PrepareLayout();
            Occupied = new StoneEquipmentProcess[item.Volume];
            Cells = new ManufacturingCell[item.Volume];
            for (int i = 0; i < item.Volume; i++)
            {
                ManufacturingCell cell = ManufacturingCellPrefab.Instantiate(content);
                Cells[i] = cell;
                cell.Index = i;
                cell.ClickAction = CellClickAction;
                cell.HoverAction = CellHoverAction;
                cell.RightClickAction = CellRightClickAction;
            }
        }

        public override void OnInitialize()
        {
            EquipmentFlow flow = Manufacture.Get<EquipmentFlow>();
            flow.SetFlowAction(64, RunProcess);
            flow.SetClearAction(64, ClearBoard);
        }

        private void ClearBoard()
        {
            Processes.Clear();
            Layout.PrepareLayout();
            Occupied = null;
            Cells = null;
        }

        private void RunProcess(EquipmentProcessData data)
        {
            List<StoneEquipmentProcess> processes = Processes
                .Where(u => u is StoneEquipmentProcess)
                .Cast<StoneEquipmentProcess>()
                .OrderBy(u => u.Position)
                .ToList();

            foreach (StoneEquipmentProcess process in processes)
            {
                process.Process(Manufacture, data);
            }
        }

        protected override IList<EquipmentProcessBase> GetSelectList()
        {
            return GameScope.Instance.Player.Role
                .GetProperty<StoneEquipmentManufacturingProperty>()
                .LearnedProcesses.Cast<EquipmentProcessBase>()
                .ToList();
        }
    }
}