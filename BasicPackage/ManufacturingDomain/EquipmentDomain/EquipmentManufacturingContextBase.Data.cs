using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using JetBrains.Annotations;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents;
using System.Collections.Generic;
using System.Linq;
using System.Ugf;
using InfinityWorldChess.GlobalDomain;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract partial class EquipmentManufacturingContextBase
		<TRaw, TProcess, TBlueprint, TContext, TTableFunction, TProperty>
	{
		public EquipmentData Data { get; private set; }

		public class EquipmentData : IEquipmentManufacturingData
		{
			private readonly TContext _context;
			private readonly IwcTableHelperFh<TProcess, TTableFunction> _processTableHelper;
			private readonly EquipmentManufacturingComponent _factory;
			public readonly Role.ItemProperty RoleItem;
			public readonly List<TProcess> Processes = new();
			public readonly float[] Property = new float[SharedConsts.EquipmentPropertyCount];

			public string EquipmentName;
			public float Endurance;
			public float Weight;
			private TProcess _selectedProcess;
			private TBlueprint _blueprint;
			private TRaw _raw;
			public TRaw Raw
			{
				get => _raw;
				set
				{
					if (_raw == value) return;
					if (_raw is not null)
						RoleItem.Add(_raw);
					if (value is not null)
						RoleItem.Remove(_raw);
					_raw = value;
					_factory.Raw.OnInitialize(_raw);
					_factory.Raw.SetFloating(_raw);
					Processes.Clear();
				}
			}
			public TBlueprint Blueprint
			{
				get => _blueprint;
				set
				{
					if (_blueprint == value) return;
					if (_blueprint is not null)
						RoleItem.Add(_raw);
					if (value is not null)
						RoleItem.Remove(_raw);
					_blueprint = value;
					_factory.Blueprint.SetFloating(_blueprint);
					_factory.Blueprint.OnInitialize(_blueprint);
				}
			}
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

			public EquipmentData([NotNull] Role mainRole, TContext context)
			{
				_context = context;
				RoleItem = mainRole.Item;
				_factory = context.Factory.Value;
				_processTableHelper = new IwcTableHelperFh<TProcess, TTableFunction>();
				_processTableHelper.BindPrepareCellAction(PrepareProcessCell);
			}

			private void PrepareProcessCell(NormalCell cell, int index)
			{
				TProcess item = _processTableHelper[index];
				cell.gameObject.GetOrAddButton(() => SelectedProcess = item);
			}

			public void SetManufacturingRole(List<TProcess> processes, FunctionalTable table)
			{
				_processTableHelper.OnInitialize(
					table,
					IwcAb.Instance.VerticalCellInk.Value, processes
				);
				SelectedProcess = processes.FirstOrDefault();
			}

			public void OnSelectBlueprintButtonClick()
			{
				GlobalScope.Instance
					.OnItemSelectionOpen(
					RoleItem.Where(u => u is TBlueprint).ToList(),
					i =>
					{
						Blueprint = i as TBlueprint;
						_context.SelectBlueprint(Blueprint);
					}
				);
			}

			public void OnSelectRawButtonClick()
			{
				GlobalScope.Instance.OnItemSelectionOpen(
					RoleItem.Where(u => u is TRaw).ToList(),
					i =>
					{
						Raw = i as TRaw;
						_context.SelectRaw(Raw);
					}
				);
			}

			public void OnShowResultButtonClick()
			{
				Equipment equipment = GetResult();
				if (equipment is not null)
					_factory.ResultContent.RefreshContent(equipment);
			}

			public void OnManufactureButtonClick()
			{
				if (EquipmentName.IsNullOrWhiteSpace())
				{
					U.T.Translate("请输入武器名称").CreateTipFloatingOnCenter();
					return;
				}

				Equipment equipment = GetResult();

				if (equipment is not null)
				{
					RoleItem.Add(equipment);
					Blueprint = null;
					Raw = default;
					_context.SelectRaw(Raw);
					_factory.ResultContent.RefreshContent(equipment);
					U.T.Translate($"成功锻造武器: {EquipmentName}").CreateTipFloatingOnCenter();
				}
				else
				{
					U.T.Translate("缺少蓝图/材料！").CreateTipFloatingOnCenter();
				}
			}

			public void OnShutdown()
			{
				if (_raw is not null)
					RoleItem.Add(_raw);
				if (_blueprint is not null)
					RoleItem.Add(_blueprint);
				
				_context.OnShutdown();
			}

			public void SetName(string text)
			{
				EquipmentName = text;
			}

			public Equipment GetResult()
			{
				if (Raw is null || Blueprint is null)
					return null;

				Equipment equipment = Blueprint.Init(_context);
				equipment.Name = EquipmentName;
				Raw.BeforeManufacturing(_context);
				_context.RunProcess(equipment);
				Raw.AfterManufacturing(_context);
				Blueprint.Finish(equipment, _context);
				return equipment;
			}

			public void AddProcess(TProcess process)
			{
				RemoveProcess(process);

				if (process is not null && Raw is not null &&
					_context.ProcessAvailable(process))
				{
					_context.AddProcessOperation(process);
					Processes.Add(process);
				}
			}

			public void RemoveProcess(TProcess process)
			{
				if (process is not null &&
					Processes.Remove(process))
					_context.RemoveProcessOperation(process);
			}
		}
	}
}