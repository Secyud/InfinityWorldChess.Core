using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.Drags
{
    public class DragManufacturePanel : MonoBehaviour, IManufacturingOperation
    {
        private DragManufactureContext _context;

        [SerializeField] private LayoutGroupTrigger ShowContent;
        [SerializeField] private ShownCell Material;
        [SerializeField] private ShownCell Blueprint;
        [SerializeField] private LayoutGroupTrigger CellContent;
        [SerializeField] private ManufacturingCell CellTemplate;

        private DragMaterial _selectedMaterial;
        private DragManufacturingProperty _property;

        private List<ItemCounter> _materialCounters;

        public DragManufacturingProperty Property
        {
            get
            {
                return _property ??= GameScope.Instance.Player.Role.Properties
                                         .Get<DragManufacturingProperty>() ??
                                     new DragManufacturingProperty();
            }
            set => _property = value;
        }


        private void Awake()
        {
            _context = DragManufactureScope.Instance.Context;
            _materialCounters = new();
        }

        public void ShowDragMessage()
        {
            string tip = CheckGenerateAccessible();

            if (tip is null)
            {
                CustomDrag drag = GenerateDrag();
                ShowContent.RefreshContent(drag);
            }
            else
            {
                tip.CreateTipFloatingOnCenter();
            }
        }

        public void ForgeAndArchiveDrag()
        {
            string tip = CheckGenerateAccessible();

            if (tip is null)
            {
                Role player = GameScope.Instance.Player.Role;
                _context.IsForging = true;
                CustomDrag drag = GenerateDrag();
                _context.IsForging = false;

                foreach (ItemCounter counter in _materialCounters)
                {
                    player.Item.Remove(counter);
                }

                _selectedMaterial = null;
                Material.BindShowable(null);

                player.Item.Add(drag);

                tip = "成功打造装备!";
            }

            tip.CreateTipFloatingOnCenter();
        }

        public void SelectMaterialClick()
        {
            IList<IItem> items = GameScope.Instance.Player.Role.Item.All();
            List<DragMaterial> materials = items.TryFindCast<IItem, DragMaterial>();
            GlobalScope.Instance.OpenSelect()
                .AutoSetSingleSelectTable<
                        DragMaterial,
                        DragMaterialSorters,
                        DragMaterialFilters>
                    (materials, SelectMaterial);
        }

        private void SelectMaterial(DragMaterial material)
        {
            _selectedMaterial = material;
            Material.BindShowable(material);
        }

        public void SelectBlueprintClick()
        {
            IList<IItem> items = GameScope.Instance.Player.Role.Item.All();
            List<DragBlueprint> materials = items.TryFindCast<IItem, DragBlueprint>();
            GlobalScope.Instance.OpenSelect()
                .AutoSetSingleSelectTable<
                        DragBlueprint,
                        DragBlueprintSorters,
                        DragBlueprintFilters>
                    (materials, SelectBlueprint);
        }

        private void SelectBlueprint(DragBlueprint blueprint)
        {
            _context.SelectedBlueprint = blueprint;
            Blueprint.BindShowable(blueprint);

            int length = blueprint?.Length ?? 0;
            int count = CellContent.RectTransform.childCount;

            if (count == length) return;

            DragMaterialContainer[] containers = new DragMaterialContainer[length];

            if (count < length)
            {
                for (int i = 0; i < count; i++)
                {
                    containers[i] = _context.Materials[i];
                }

                for (int i = count; i < length; i++)
                {
                    containers[i] = new DragMaterialContainer((byte)i);
                    ManufacturingCell cell = CellTemplate.Instantiate(CellContent.RectTransform);
                    cell.CellIndex = i;
                    cell.Operation = this;
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    containers[i] = _context.Materials[i];
                }

                for (int i = length; i < count; i++)
                {
                    Transform trans = CellContent.RectTransform.GetChild(i);
                    Destroy(trans.gameObject);
                }
            }

            _context.Materials = containers;

            CellContent.enabled = true;
        }

        public void OnLeftClick(int index)
        {
            SelectMaterial(index, _selectedMaterial);
        }

        public void OnHover(int index)
        {
            var container = _context.Materials[index];
            if (container.Occupied)
            {
                container = _context.Materials[container.MaterialP];
                container.Material.CreateAutoCloseFloatingOnMouse();
            }
        }

        public void OnRightClick(int index)
        {
            SelectMaterial(index, null);
        }

        private void SelectMaterial(int index, DragMaterial material)
        {
            int start;
            int end;
            if (material is null)
            {
                DragMaterialContainer container = _context.Materials[index];
                if (!container.Occupied) return;

                DragMaterial tmp = _context.Materials[container.MaterialP].Material;
                end = tmp.Length + container.MaterialP;
                start = container.MaterialP;
            }
            else
            {
                end = material.Length + index;
                start = index;
            }

            for (int i = start; i < end; i++)
            {
                Transform cellTransform = CellContent.RectTransform.GetChild(i);
                ManufacturingCell cell = cellTransform.GetComponent<ManufacturingCell>();
                cell.BindShowable(material);

                if (_context.Materials[i].Occupied)
                {
                    SelectMaterial(i, null);
                }

                _context.Materials[i].SetProcess(material, (byte)index);
            }
        }

        private string CheckGenerateAccessible()
        {
            _materialCounters.Clear();

            foreach (DragMaterialContainer container in _context.Materials)
            {
                if (container.Material is not null)
                {
                    var id = container.Material.ResourceId;
                    ItemCounter counter = _materialCounters.FirstOrDefault(
                        u => u.Item.ResourceId == id);

                    if (counter is null)
                    {
                        _materialCounters.AddLast(new(container.Material, 1));
                    }
                    else
                    {
                        counter.Count += 1;
                    }
                }
            }

            foreach (ItemCounter counter in _materialCounters)
            {
                if (counter.Item.Quantity < counter.Count)
                {
                    return $"材料不足: {counter.Item.Name}({counter.Count}/{counter.Item.Quantity})";
                }
            }

            if (_context.SelectedBlueprint is null)
            {
                return "请选择锻造图纸!";
            }

            return null;
        }

        private CustomDrag GenerateDrag()
        {
            CustomDrag customDrag = _context.SelectedBlueprint.InitDrag();

            foreach (DragMaterialContainer container in _context.Materials)
            {
                container?.Material?.ProcessDrag(customDrag);
            }

            return customDrag;
        }
    }
}