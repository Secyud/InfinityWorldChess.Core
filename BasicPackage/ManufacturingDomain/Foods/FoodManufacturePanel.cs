using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.ManufacturingDomain.Drags;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.Foods
{
    public class FoodManufacturePanel : MonoBehaviour, IManufacturingOperation
    {
        private FoodManufactureContext _context;

        [SerializeField] private LayoutGroupTrigger ShowContent;
        [SerializeField] private ShownCell Material;
        [SerializeField] private ShownCell Blueprint;
        [SerializeField] private ShownCell Drag;
        [SerializeField] private LayoutGroupTrigger CellContent;
        [SerializeField] private ManufacturingCell CellTemplate;

        private DragMaterial _selectedDrag;
        private FoodManufacturingProperty _property;
        private List<ItemCounter> _materialCounters;

        public FoodManufacturingProperty Property
        {
            get
            {
                return _property ??= GameScope.Instance.Player.Role.Properties
                                         .Get<FoodManufacturingProperty>() ??
                                     new FoodManufacturingProperty();
            }
            set => _property = value;
        }

        private void Awake()
        {
            _context = FoodManufactureScope.Instance.Context;
            _materialCounters = new();
        }

        public void ShowFoodMessage()
        {
            string tip = CheckGenerateAccessible();

            if (tip is null)
            {
                CustomFood food = GenerateFood();
                ShowContent.RefreshContent(food);
            }
            else
            {
                tip.CreateTipFloatingOnCenter();
            }
        }

        public void ForgeAndArchiveFood()
        {
            string tip = CheckGenerateAccessible();

            if (tip is null)
            {
                Role player = GameScope.Instance.Player.Role;
                _context.IsForging = true;
                CustomFood food = GenerateFood();
                _context.IsForging = false;
                foreach (ItemCounter counter in _materialCounters)
                {
                    player.Item.Remove(counter);
                }

                _selectedDrag = null;
                Drag.BindShowable(null);
                
                player.Item.Remove(_context.SelectedMaterial, 1);
                SelectMaterial(null);
                player.Item.Add(food);

                tip = "成功打造装备!";
            }

            tip.CreateTipFloatingOnCenter();
        }

        public void SelectMaterialClick()
        {
            IList<IItem> items = GameScope.Instance.Player.Role.Item.All();
            List<FoodMaterial> materials = items.TryFindCast<IItem, FoodMaterial>();
            GlobalScope.Instance.OpenSelect()
                .AutoSetSingleSelectTable<
                        FoodMaterial,
                        FoodMaterialSorters,
                        FoodMaterialFilters>
                    (materials, SelectMaterial);
        }

        private void SelectMaterial(FoodMaterial material)
        {
            _context.SelectedMaterial = material;
            Material.BindShowable(material);

            int length = material?.Length ?? 0;
            int count = CellContent.RectTransform.childCount;

            if (count == length) return;

            DragMaterialContainer[] containers = new DragMaterialContainer[length];

            if (count < length)
            {
                for (int i = 0; i < count; i++)
                {
                    containers[i] = _context.DragMaterials[i];
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
                    containers[i] = _context.DragMaterials[i];
                }

                for (int i = length; i < count; i++)
                {
                    Transform trans = CellContent.RectTransform.GetChild(i);
                    Destroy(trans.gameObject);
                }
            }

            _context.DragMaterials = containers;

            CellContent.enabled = true;
        }

        public void SelectBlueprintClick()
        {
            GlobalScope.Instance.OpenSelect()
                .AutoSetSingleSelectTable<
                        FoodBlueprint,
                        FoodBlueprintSorters,
                        FoodBlueprintFilters>
                    (Property.LearnedBlueprints, SelectBlueprint);
        }

        private void SelectBlueprint(FoodBlueprint blueprint)
        {
            _context.SelectedBlueprint = blueprint;
            Blueprint.BindShowable(blueprint);
        }

        public void SelectDragClick()
        {
            IList<IItem> items = GameScope.Instance.Player.Role.Item.All();
            List<DragMaterial> materials = items.TryFindCast<IItem, DragMaterial>();
            GlobalScope.Instance.OpenSelect()
                .AutoSetSingleSelectTable<
                        DragMaterial,
                        DragMaterialSorters,
                        DragMaterialFilters>
                    (materials, SelectDrag);
        }

        private void SelectDrag(DragMaterial process)
        {
            _selectedDrag = process;
            Drag.BindShowable(process);
        }

        public void OnLeftClick(int index)
        {
            SelectDrag(index, _selectedDrag);
        }

        public void OnHover(int index)
        {
            var container = _context.DragMaterials[index];
            if (container.Occupied)
            {
                container = _context.DragMaterials[container.MaterialP];
                container.Material.CreateAutoCloseFloatingOnMouse();
            }
        }

        public void OnRightClick(int index)
        {
            SelectDrag(index, null);
        }

        private void SelectDrag(int index, DragMaterial material)
        {
            int start;
            int end;
            if (material is null)
            {
                DragMaterialContainer container = _context.DragMaterials[index];
                if (!container.Occupied) return;

                DragMaterial tmp = _context.DragMaterials[container.MaterialP].Material;
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
                cell.Icon.Sprite = material?.Cell?.Value;

                if (_context.DragMaterials[i].Occupied)
                {
                    SelectDrag(i, null);
                }

                _context.DragMaterials[i].SetProcess(material, (byte)index);
            }
        }

        private string CheckGenerateAccessible()
        {
            if (_context.SelectedMaterial is null)
            {
                return "请选择材料!";
            }

            if (_context.SelectedBlueprint is null)
            {
                return "请选择锻造图纸!";
            }
            
            
            _materialCounters.Clear();

            foreach (DragMaterialContainer container in _context.DragMaterials)
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

            return null;
        }

        private CustomFood GenerateFood()
        {
            CustomFood customFood = _context.SelectedBlueprint.InitFood();

            _context.SelectedMaterial.StartFoodManufacturing(customFood);

            foreach (DragMaterialContainer container in _context.DragMaterials)
            {
                container?.Material?.ProcessFood(customFood);
            }

            _context.SelectedMaterial.FinishFoodManufacturing(customFood);

            return customFood;
        }
    }
}