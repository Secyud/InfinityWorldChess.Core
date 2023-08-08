using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.MetalDomain
{
    public class MetalProcessFunction : EquipmentProcessSelect
    {
        [SerializeField] private LayoutGroupTrigger Layout;
        [SerializeField] private ManufacturingCell ManufacturingCellPrefab;
        public int Temperature { get; private set; }
        public MetalEquipmentProcess[] Occupied { get; private set; }
        public ManufacturingCell[] Cells { get; private set; }

        private const int MeltingPointFactor = 512;


        private void CellRightClickAction(ManufacturingCell cell)
        {
            MetalEquipmentProcess process = Occupied[cell.Index];

            if (process is not null)
            {
                for (int i = 0; i < process.Length; i++)
                {
                    int position = process.StartPosition + i;
                    Occupied[position] = null;
                    Cells[i].Image.color = Color.white;
                }

                Processes.Remove(process);
            }
        }
        
        private void CellClickAction(ManufacturingCell cell)
        {
            CellRightClickAction(cell);
            
            if (SelectedItem is not MetalEquipmentProcess process)
                return;

            for (int i = 0; i < process.Length; i++)
            {
                int position = process.StartPosition + i;
                Occupied[position] = process;
                Cells[i].Image.color = process.Color;
            }

            Processes.Add(process);
        }

        private void CellHoverAction(ManufacturingCell cell)
        {
            MetalEquipmentProcess process = Occupied[cell.Index];

            process?.CreateAutoCloseFloatingOnMouse();
        }

        public void ReselectRaw(MetalEquipmentRaw item)
        {
            RectTransform content = Layout.PrepareLayout();
            int length = Math.Min(254, item.MeltingPoint / MeltingPointFactor + 1);
            Occupied = new MetalEquipmentProcess[length];
            Cells = new ManufacturingCell[length];
            for (int i = 0; i < length; i++)
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
            MetalEquipmentRaw raw = Manufacture.Get<EquipmentRawSelect>().SelectedItem as MetalEquipmentRaw;

            Temperature = raw!.MeltingPoint;

            List<MetalEquipmentProcess> processes = Processes
                .Where(u => u is MetalEquipmentProcess)
                .Cast<MetalEquipmentProcess>()
                .OrderBy(u => u.StartPosition)
                .ToList();

            int start = 0;
            foreach (MetalEquipmentProcess process in processes)
            {
                process.Process(Manufacture, data);
                Temperature -= MeltingPointFactor * (process.StartPosition - start);
                start = process.StartPosition;
            }
        }

        protected override IList<EquipmentProcessBase> GetSelectList()
        {
            return GameScope.Instance.Player.Role
                .GetProperty<MetalEquipmentManufacturingProperty>()
                .LearnedProcesses.Cast<EquipmentProcessBase>()
                .ToList();
        }
    }
}