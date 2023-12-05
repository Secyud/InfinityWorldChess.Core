using System.Collections.Generic;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.Equipments
{
    public class EquipmentManufacturePanel : MonoBehaviour, IManufacturingOperation
    {
        private EquipmentManufactureContext _context;

        [SerializeField] private LayoutGroupTrigger ShowContent;
        [SerializeField] private ShownCell Material;
        [SerializeField] private ShownCell Blueprint;
        [SerializeField] private ShownCell Process;
        [SerializeField] private LayoutGroupTrigger CellContent;
        [SerializeField] private ManufacturingCell CellTemplate;

        private EquipmentProcess _selectedProcess;
        private EquipmentManufacturingProperty _property;

        public EquipmentManufacturingProperty Property
        {
            get
            {
                return _property ??= GameScope.Instance.Player.Role.Properties
                                         .Get<EquipmentManufacturingProperty>() ??
                                     new EquipmentManufacturingProperty();
            }
            set => _property = value;
        }

        private void Awake()
        {
            _context = EquipmentManufactureScope.Instance.Context;
        }

        public void Close()
        {
            U.M.DestroyScope<EquipmentManufactureScope>();
        }

        public void ShowEquipmentMessage()
        {
            string tip = CheckGenerateAccessible();

            if (tip is null)
            {
                CustomEquipment equipment = GenerateEquipment();
                ShowContent.RefreshContent(equipment);
            }
            else
            {
                tip.CreateTipFloatingOnCenter();
            }
        }

        public void CreateAndArchiveEquipment()
        {
            string tip = CheckGenerateAccessible();

            if (tip is null)
            {
                Role player = GameScope.Instance.Player.Role;
                _context.IsForging = true;
                CustomEquipment equipment = GenerateEquipment();
                _context.IsForging = false;
                player.Item.Remove(_context.SelectedMaterial, 1);
                SelectMaterial(null);
                player.Item.Add(equipment);

                tip = "成功打造装备!";
            }

            tip.CreateTipFloatingOnCenter();
        }

        public void SelectMaterialClick()
        {
            IList<IItem> items = GameScope.Instance.Player.Role.Item.All();
            List<EquipmentMaterial> materials = items.TryFindCast<IItem, EquipmentMaterial>();
            GlobalScope.Instance.OpenSelect()
                .AutoSetSingleSelectTable<
                        EquipmentMaterial,
                        EquipmentMaterialSorters,
                        EquipmentMaterialFilters>
                    (materials, SelectMaterial);
        }

        private void SelectMaterial(EquipmentMaterial material)
        {
            _context.SelectedMaterial = material;
            Material.BindShowable(material);

            int length = material?.Length ?? 0;
            int count = CellContent.RectTransform.childCount;

            if (count == length) return;

            EquipmentProcessContainer[] containers = new EquipmentProcessContainer[length];

            if (count < length)
            {
                for (int i = 0; i < count; i++)
                {
                    containers[i] = _context.Processes[i];
                }

                for (int i = count; i < length; i++)
                {
                    containers[i] = new EquipmentProcessContainer((byte)i);
                    ManufacturingCell cell = CellTemplate.Instantiate(CellContent.RectTransform);
                    cell.CellIndex = i;
                    cell.Operation = this;
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    containers[i] = _context.Processes[i];
                }

                for (int i = length; i < count; i++)
                {
                    Transform trans = CellContent.RectTransform.GetChild(i);
                    Destroy(trans.gameObject);
                }
            }

            _context.Processes = containers;

            CellContent.enabled = true;
        }

        public void SelectBlueprintClick()
        {
            GlobalScope.Instance.OpenSelect()
                .AutoSetSingleSelectTable<
                        EquipmentBlueprint,
                        EquipmentBlueprintSorters,
                        EquipmentBlueprintFilters>
                    (Property.LearnedBlueprints, SelectBlueprint);
        }

        private void SelectBlueprint(EquipmentBlueprint blueprint)
        {
            _context.SelectedBlueprint = blueprint;
            Blueprint.BindShowable(blueprint);
        }

        public void SelectProcessClick()
        {
            GlobalScope.Instance.OpenSelect()
                .AutoSetSingleSelectTable<
                        EquipmentProcess,
                        EquipmentProcessSorters,
                        EquipmentProcessFilters>
                    (Property.LearnedProcesses, SelectProcess);
        }

        private void SelectProcess(EquipmentProcess process)
        {
            _selectedProcess = process;
            Process.BindShowable(process);
        }

        public void OnLeftClick(int index)
        {
            SelectProcess(index, _selectedProcess);
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

        private void SelectProcess(int index, EquipmentProcess process)
        {
            int start;
            int end;
            if (process is null)
            {
                EquipmentProcessContainer container = _context.Processes[index];
                if (!container.Occupied) return;

                EquipmentProcess tmp = _context.Processes[container.ProcessP].Process;
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
                cell.Icon.Sprite = process?.Cell?.Value;

                if (_context.Processes[i].Occupied)
                {
                    SelectProcess(i, null);
                }

                _context.Processes[i].SetProcess(process, (byte)index);
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

        private CustomEquipment GenerateEquipment()
        {
            CustomEquipment customEquipment = _context.SelectedBlueprint.InitEquipment();

            _context.SelectedMaterial.StartEquipmentManufacturing(customEquipment);

            foreach (EquipmentProcessContainer process in _context.Processes)
            {
                process?.Process?.Process(customEquipment, _context.SelectedMaterial);
            }

            _context.SelectedMaterial.FinishEquipmentManufacturing(customEquipment);

            return customEquipment;
        }
    }
}