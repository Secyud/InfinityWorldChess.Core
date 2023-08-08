using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ManufacturingDomain.Components;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.FlavorDomain
{
    public abstract class FlavorSelect<TProcessData, TFlavor> :
        Select<IItem, ItemSorters, ItemFilters>
        where TProcessData : FlavorProcessData, new()
        where TFlavor : IItem
    {
        [SerializeField] private LayoutGroupTrigger Layout;
        [SerializeField] private ManufacturingCell ManufacturingCellPrefab;
        public FlavorRaw[] Occupied { get; private set; }
        public ManufacturingCell[] Cells { get; private set; }

        protected virtual void Awake()
        {
            ClearBoard();
        }

        private void CellRightClickAction(ManufacturingCell cell)
        {
            FlavorRaw raw = Occupied[cell.Index];

            if (raw is not null)
            {
                Occupied[cell.Index] = null;
                Cells[cell.Index].Image.color = Color.white;

                IList<IItem> itemProvider = GetFlow().GetItemProvider();
                itemProvider.Add(raw);
            }
        }

        private void CellClickAction(ManufacturingCell cell)
        {
            CellRightClickAction(cell);

            if (SelectedItem is not FlavorRaw raw)
                return;

            Occupied[cell.Index] = raw;
            Cells[cell.Index].Image.color = raw.Color;

            IList<IItem> itemProvider = GetFlow().GetItemProvider();
            itemProvider.Remove(raw);
        }

        private void CellHoverAction(ManufacturingCell cell)
        {
            FlavorRaw raw = Occupied[cell.Index];

            raw?.CreateAutoCloseFloatingOnMouse();
        }

        protected abstract FlavorFlow<TProcessData, TFlavor> GetFlow();

        public override void OnInitialize()
        {
            FlavorFlow<TProcessData, TFlavor> flow = GetFlow();
            flow.SetClearAction(65, ClearBoard);
        }

        private void ClearBoard()
        {
            IList<IItem> itemProvider = GetFlow().GetItemProvider();
            foreach (FlavorRaw raw in Occupied)
            {
                if (raw is not null)
                    itemProvider.Add(raw);
            }

            Layout.PrepareLayout();
            RectTransform content = Layout.PrepareLayout();
            Occupied = new FlavorRaw[BasicConsts.FlavorTime];
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

        protected override IList<IItem> GetSelectList()
        {
            return GameScope.Instance.Player.Role.Item
                .Where(u => u is FlavorRaw)
                .ToList();
        }
        
        
        public void Manufacturing(int index,TProcessData data)
        {
            if (Occupied[index] is not null)
                Occupied[index].Manufacturing(Manufacture,data);
        }
    }
}