using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.TableComponents;
using System.Collections.Generic;
using System.Linq;
using System.Ugf;
using InfinityWorldChess.GlobalDomain;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract partial class FlavorManufacturingContextBase
		<TFlavor, TComponent, TProcess, TContext, TProcessTf, TProperty>
	{
		public FlavorData Data { get; private set; }

		public PrefabContainer<FlavorCell> FlavorCell;

		protected FlavorManufacturingContextBase(IwcAb ab)
		{
			Factory = MonoContainer<TComponent>.Create(ab);
			FlavorCell = PrefabContainer<FlavorCell>.Create(ab);
		}

		public class FlavorData : IFlavorManufacturingData
		{
			private readonly IwcTableHelperFh<TProcess, TProcessTf> _processTableHelper = new();
			private readonly FlavorTableHelper<TProcess> _addedProcessTableHelper = new();
			private readonly TContext _context;
			private readonly TComponent _factory;
			public Role.ItemProperty RoleItem;
			public readonly List<TProcess> Processes = new();
			public readonly List<TProcess> AddedProcesses = new();
			private TProcess _selectedProcess;

			public TProcess SelectedProcess
			{
				get => _selectedProcess;
				set
				{
					_selectedProcess = value;
					_factory.Process.OnInitialize(_selectedProcess);
					_factory.Process.SetFloating(_selectedProcess);
				}
			}

			public string Name { get; set; }

			public int Time { get; private set; }

			public FlavorData(Role mainRole, TContext context)
			{
				RoleItem = mainRole.Item;
				_context = context;
				_factory = context.Factory.Value;
				_processTableHelper.BindPrepareCellAction(PrepareProcessCell);
				_addedProcessTableHelper.BindPrepareCellAction(PrepareAddedProcessCell);
				_addedProcessTableHelper.OnInitialize(
					_factory.AddedProcessesTable,
					context.FlavorCell.Value, AddedProcesses
				);
			}

			private void PrepareProcessCell(NormalCell cell, int index)
			{
				TProcess item = _processTableHelper[index];
				cell.gameObject.GetOrAddButton(() => SelectedProcess = item);
			}

			private void PrepareAddedProcessCell(FlavorCell cell, int index)
			{
				TProcess item = _addedProcessTableHelper[index];
				cell.gameObject.GetOrAddButton(() => RemoveProcess(item));
				cell.Flavor.gameObject.GetOrAddButton(() => OnProcessFlavorSelect(item));
			}

			public void SetManufacturingRole(List<TProcess> processes, FunctionalTable table)
			{
				_processTableHelper.OnInitialize(
					table,
					IwcAb.Instance.VerticalCellInk.Value, processes
				);
				SelectedProcess = processes.FirstOrDefault();
			}

			public void OnProcessFlavorSelect(TProcess process)
			{
				GlobalScope.Instance.OnItemSelectionOpen(
					RoleItem.Where(u => u is IHasFlavor).ToList(),
					i =>
					{
						if (process.Flavor is not null)
							RoleItem.Add(process.Flavor as IItem);

						process.Flavor = i as IHasFlavor;

						RoleItem.Remove(i);
					}
				);
			}

			public void OnAddProcessClick()
			{
				if (SelectedProcess is null)
					return;

				SelectedProcess.TimeAdd = Time;
				AddProcess(SelectedProcess);
			}

			public void SetTime(float f)
			{
				Time = (int)f;
			}

			public void OnShowResultButtonClick()
			{
				TFlavor content = GetResult();
				if (content is not null)
					_factory.ResultContent.RefreshContent(content);
			}

			public void OnManufactureButtonClick()
			{
				if (Name.IsNullOrWhiteSpace())
				{
					U.T.Translate("请输入名称").CreateTipFloatingOnCenter();
					return;
				}

				TFlavor flavor = GetResult();
				if (flavor is null)
				{
					U.T.Translate("请添加必须材料").CreateTipFloatingOnCenter();
					return;
				}

				RoleItem.Add(flavor);
				AddedProcesses.Clear();
				_factory.AddedProcessesTable.RefreshFilter();
				_factory.ResultContent.RefreshContent(flavor);
				U.T.Translate($"获得: {Name}").CreateTipFloatingOnCenter();
			}

			public void OnShutdown()
			{
				foreach (TProcess process in AddedProcesses)
				{
					if (process.Flavor is IItem item)
						RoleItem.Add(item);
				}
				_context.OnShutdown();
			}

			public void SetName(string text)
			{
				Name = text;
			}


			public TFlavor GetResult()
			{
				TFlavor flavor = _context.InitFlavor();
				if (flavor is null)
					return null;

				flavor.Name = Name;
				_context.RunProcess(flavor);
				return flavor;
			}


			public void AddProcess(TProcess process)
			{
				RemoveProcess(process);
				if (process is not null)
				{
					int count = Processes.Count;
					int i = 0;
					for (; i < count; i++)
					{
						if (Processes[i].TimeAdd > process.TimeAdd)
							break;
					}
					Processes.Insert(i, process);
				}
			}

			public void RemoveProcess(TProcess process)
			{
				if (process is not null && Processes.Remove(process))
					process.Flavor = null;
				_factory.AddedProcessesTable.RefreshFilter();
			}
		}
	}
}