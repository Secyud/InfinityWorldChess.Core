using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;
using UnityEngine.UI;

namespace InfinityWorldChess.ManufacturingDomain.WoodDomain
{
    public class WoodProcessFunction : EquipmentProcessSelect
    {
        [SerializeField] private LayoutGroupTrigger Layout;
        [SerializeField] private ManufacturingCell ManufacturingCellPrefab;
        [SerializeField] private GridLayoutGroup LayoutGroup;

        public WoodEquipmentProcess[] Occupied { get; private set; }
        public ManufacturingCell[] Cells { get; private set; }

        public byte Width { get; private set; }
        public byte Height { get; private set; }

        private void CellRightClickAction(ManufacturingCell cell)
        {
            WoodEquipmentProcess process = Occupied[cell.Index];
            if (process is null) return;

            int positionX = process.Position % Width;
            int positionY = process.Position / Width;

            for (int i = 0; i < process.RangeX; i++)
            {
                for (int j = 0; j < process.RangeY; j++)
                {
                    int position = (positionY + j) * Width + positionX + i;

                    Occupied[position] = null;
                    Cells[position].Image.color = Color.white;
                }
            }

            Processes.Remove(process);
        }

        private void CellClickAction(ManufacturingCell cell)
        {
            CellRightClickAction(cell);
            
            WoodEquipmentProcess process = SelectedItem as WoodEquipmentProcess;

            int positionX = cell.Index % Width;
            int positionY = cell.Index / Width;

            if (process is null ||
                process.RangeX + positionX >= Width ||
                process.RangeY + positionY >= Height)
            {
                "超出范围".CreateTipFloatingOnCenter();
                return;
            }

            process.Position = (short)cell.Index;

            for (int i = 0; i < process.RangeX; i++)
            {
                for (int j = 0; j < process.RangeY; j++)
                {
                    int position = (positionY + j) * Width + positionX + i;

                    Occupied[position] = process;
                    Cells[position].Image.color = process.Color;
                }
            }

            Processes.Add(process);
        }

        private void CellHoverAction(ManufacturingCell cell)
        {
            WoodEquipmentProcess process = Occupied[cell.Index];

            process?.CreateAutoCloseFloatingOnMouse();
        }

        public void ReselectRaw(WoodEquipmentRaw item)
        {
            RectTransform content = Layout.PrepareLayout();
            Width = item.Width;
            Height = item.Height;
            LayoutGroup.constraintCount = Width;
            int length = Width * Height;

            Occupied = new WoodEquipmentProcess[length];
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
            List<WoodEquipmentProcess> processes = Processes
                .Where(u => u is WoodEquipmentProcess)
                .Cast<WoodEquipmentProcess>()
                .OrderBy(u => u.Position)
                .ToList();

            foreach (WoodEquipmentProcess process in processes)
            {
                process.Process(Manufacture, data);
            }
        }

        protected override IList<EquipmentProcessBase> GetSelectList()
        {
            return GameScope.Instance.Player.Role
                .GetProperty<WoodEquipmentManufacturingProperty>()
                .LearnedProcesses.Cast<EquipmentProcessBase>()
                .ToList();
        }
    }
}