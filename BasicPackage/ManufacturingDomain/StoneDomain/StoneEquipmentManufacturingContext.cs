#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.Layout;
using System.Linq;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public class StoneEquipmentManufacturingContext : EquipmentManufacturingContextBase<
			StoneEquipmentRaw, StoneEquipmentManufacturingProcess, StoneEquipmentManufacturingBlueprint,
			StoneEquipmentManufacturingContext, StoneEquipmentManufacturingProcessTf,
			StoneEquipmentManufacturingProperty>
	{

		public StoneEquipmentData StoneData { get; private set; }

		public class StoneEquipmentData
		{
			public bool[] Occupied { get; private set; }

			public SButton[] Cells { get; private set; }

			private readonly StoneEquipmentManufacturingContext _context;
			private readonly EquipmentData _data;
			private readonly LayoutGroupTrigger _layout;

			public StoneEquipmentData(StoneEquipmentManufacturingContext context)
			{
				_context = context;
				_data = context.Data;
				_layout = _context.Factory.Value.CellContent;
			}

			public void Set(short position)
			{
				Occupied[position] = false;
				SButton cell = Cells[position];
				cell.GetComponent<SImage>().color = Color.white;
				cell.Bind(
					() =>
					{
						StoneEquipmentManufacturingProcess process = _data.SelectedProcess;
						process.Position = position;
						_data.AddProcess(process);
					}
				);
			}

			public void Set(StoneEquipmentManufacturingProcess process)
			{
				Occupied[process.Position] = true;
				SButton cell = Cells[process.Position];
				cell.GetComponent<SImage>().color = process.Color;
				cell.Bind(() => _data.RemoveProcess(process));
			}

			public void Create(byte length)
			{
				RectTransform content = _layout.PrepareLayout();
				byte cellCount = length;
				Occupied = new bool[length];
				Cells = new SButton[length];
				for (byte i = 0; i < cellCount; i++)
				{
					Cells[i] = _context.ManufacturingCell.Instantiate(content);
					Set(i);
				}
			}

			public void Delete()
			{
				_layout.PrepareLayout();
				Occupied = null;
				Cells = null;
			}
		}

		public StoneEquipmentManufacturingContext(IwcAb ab) : base(ab)
		{
		}

		protected override void SelectBlueprint(StoneEquipmentManufacturingBlueprint blueprint)
		{
		}

		protected override void SelectRaw(StoneEquipmentRaw stoneEquipmentRaw)
		{
			if (stoneEquipmentRaw is null)
				StoneData.Delete();
			else
				StoneData.Create(stoneEquipmentRaw.Volume);
		}

		protected override bool ProcessAvailable(StoneEquipmentManufacturingProcess process)
		{
			return process.Position < Data.Raw.Volume &&
				!StoneData.Occupied[process.Position];
		}

		protected override void AddProcessOperation(StoneEquipmentManufacturingProcess process)
		{
			StoneData.Set(process);
		}

		protected override void RemoveProcessOperation(StoneEquipmentManufacturingProcess process)
		{
			StoneData.Set(process.Position);
		}

		protected override void RunProcess( Equipment equipment)
		{
			foreach (StoneEquipmentManufacturingProcess process in Data.Processes
				.OrderBy(u => u.Position).ToList())
				process.Process(this,equipment);
		}

		public override bool OnCreation(Role mainRole, Role manufacturingRole = null)
		{
			if (!base.OnCreation(mainRole, manufacturingRole)) return false;

			StoneData = new StoneEquipmentData(this);
			return true;
		}

		public override void OnShutdown()
		{
			base.OnShutdown();
			StoneData = null;
		}
	}
}