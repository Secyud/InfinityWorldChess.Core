using System.Collections.Generic;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemTemplates;
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
        [SerializeField] private ShownCell Process;
        [SerializeField] private LayoutGroupTrigger CellContent;
        [SerializeField] private ManufacturingCell CellTemplate;

        private FoodProcess _selectedProcess;
        private List<FoodProcess> _processes;
        
        private void Awake()
        {
            _context = FoodManufactureScope.Instance.Context;
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
                player.Item.Remove(_context.SelectedMaterial, 1);
                _context.SelectedMaterial = null;
                player.Item.Remove(_context.SelectedBlueprint, 1);
                _context.SelectedBlueprint = null;
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

            if (material is null)
            {
                _context.Processes = null;
            }
            else
            {
                int count = CellContent.RectTransform.childCount;

                if (count == material.Length)
                {
                    return;
                }

                FoodProcessContainer[] containers = new FoodProcessContainer[material.Length];

                if (count < material.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        containers[i] = _context.Processes[i];
                    }

                    for (int i = count; i < material.Length; i++)
                    {
                        containers[i] = new FoodProcessContainer((byte)i);
                        ManufacturingCell cell = CellTemplate.Instantiate(CellContent.RectTransform);
                        cell.CellIndex = i;
                        cell.Operation = this;
                    }
                }
                else
                { 
                    for (int i = 0; i < material.Length; i++)
                    {
                        containers[i] = _context.Processes[i];
                    }

                    for (int i = material.Length; i <count ; i++)
                    {
                        Transform trans = CellContent.RectTransform.GetChild(i);
                        Destroy(trans.gameObject);
                    }
                }

                _context.Processes = containers;

                CellContent.enabled = true;
            }
        }
        public void SelectBlueprintClick()
        {
            IList<IItem> items = GameScope.Instance.Player.Role.Item.All();
            List<FoodBlueprint> materials = items.TryFindCast<IItem, FoodBlueprint>();
            GlobalScope.Instance.OpenSelect()
                .AutoSetSingleSelectTable<
                        FoodBlueprint,
                        FoodBlueprintSorters,
                        FoodBlueprintFilters>
                    (materials, SelectBlueprint);
        }
        private void SelectBlueprint(FoodBlueprint blueprint)
        {
            _context.SelectedBlueprint = blueprint;
            Blueprint.BindShowable(blueprint);
        }
        public void SelectProcessClick()
        {
            if (_processes is null)
            {
                PropertyCollection<Role, IRoleProperty> properties = GameScope.Instance.Player.Role.Properties;
                _processes = properties
                    .Get<FoodManufacturingProperty>()?
                    .LearnedProcesses ?? new List<FoodProcess>();
            }

            GlobalScope.Instance.OpenSelect()
                .AutoSetSingleSelectTable<
                        FoodProcess,
                        FoodProcessSorters,
                        FoodProcessFilters>
                    (_processes, SelectProcess);
        }
        private void SelectProcess(FoodProcess process)
        {
            _selectedProcess = process;
            Process.BindShowable(process);
        }
        public void OnLeftClick(int index)
        {
            SelectProcess(index,_selectedProcess);
        }
        public void OnHover(int index)
        {
            var container = _context.Processes[index];
            if (container.Occupied)
            {
                container = _context.Processes[container.ProcessP];
                container.Process.CreateAutoCloseFloatingOnMouse();
            }
        }
        public void OnRightClick(int index)
        {
            SelectProcess(index, null);
        }
        private void SelectProcess(int index, FoodProcess process)
        {
            int start;
            int end;
            if (process is null)
            {
                FoodProcessContainer container = _context.Processes[index];
                if (!container.Occupied) return;

                FoodProcess tmp = _context.Processes[container.ProcessP].Process;
                end = tmp.Length + container.ProcessP;
                start = container.ProcessP;
            }
            else
            {
                end = process.Length + index;
                start = index;
            }
            
            for (int i = start; i < end; i++)
            {
                Transform cellTransform = CellContent.RectTransform.GetChild(i);
                ManufacturingCell cell = cellTransform.GetComponent<ManufacturingCell>();
                cell.BindShowable(process);

                if (_context.Processes[i].Occupied)
                {
                    SelectProcess(i, null);
                }
                
                _context.Processes[i].SetProcess(process,(byte)index);
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

            return null;
        }
        private CustomFood GenerateFood()
        {
            CustomFood customFood = _context.SelectedBlueprint.InitFood();

            _context.SelectedMaterial.StartFoodManufacturing(customFood);

            foreach (FoodProcessContainer process in _context.Processes)
            {
                process?.Process?.Process(customFood, _context.SelectedMaterial);
            }

            _context.SelectedMaterial.FinishFoodManufacturing(customFood);

            return customFood;
        }
    }
}