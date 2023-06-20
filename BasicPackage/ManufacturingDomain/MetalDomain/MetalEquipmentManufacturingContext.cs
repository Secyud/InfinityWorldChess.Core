#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.Layout;
using System.Linq;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public class MetalEquipmentManufacturingContext : EquipmentManufacturingContextBase<
		MetalEquipmentRaw, MetalEquipmentManufacturingProcess, MetalEquipmentManufacturingBlueprint,
		MetalEquipmentManufacturingContext, MetalEquipmentManufacturingProcessTf,
		MetalEquipmentManufacturingProperty>, ISingleton
	{

		public MetalEquipmentData MetalData { get; private set; }

		public class MetalEquipmentData
		{
			public bool[] Occupied;
			public SButton[] Cells;
			public int Temperature;
			private readonly MetalEquipmentManufacturingContext _context;
			private readonly EquipmentData _data;
			private readonly LayoutGroupTrigger _layout;

			public MetalEquipmentData(MetalEquipmentManufacturingContext context)
			{
				_context = context;
				_data = context.Data;
				_layout = _context.Factory.Value.CellContent;
			}

			public void Set(byte position)
			{
				Occupied[position] = false;
				SButton cell = Cells[position];
				cell.GetComponent<SImage>().color = Color.white;
				cell.Bind(
					() =>
					{
						MetalEquipmentManufacturingProcess process = _data.SelectedProcess;
						process.StartPosition = position;
						_data.AddProcess(process);
					}
				);
			}

			public void Set(MetalEquipmentManufacturingProcess process)
			{
				for (int i = process.StartPosition; i < process.StartPosition + process.Length; i++)
				{
					Occupied[i] = true;
					SButton cell = Cells[i];
					cell.GetComponent<SImage>().color = process.Color;
					cell.Bind(() => _data.RemoveProcess(process));
				}
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

		public MetalEquipmentManufacturingContext(IwcAb ab) : base(ab)
		{
		}

		protected override void SelectBlueprint(MetalEquipmentManufacturingBlueprint blueprint)
		{
		}

		protected override void SelectRaw(MetalEquipmentRaw metalEquipmentRaw)
		{
			if (metalEquipmentRaw is null)
				MetalData.Delete();
			else
			{
				byte cellCount = GetForgeCellCount(metalEquipmentRaw);
				MetalData.Create(cellCount);
			}
		}

		protected override bool ProcessAvailable(MetalEquipmentManufacturingProcess process)
		{
			if (process.StartPosition + process.Length > MetalData.Occupied.Length)
				return false;

			for (int i = process.StartPosition; i < process.StartPosition + process.Length; i++)
				if (MetalData.Occupied[i])
					return false;

			return true;
		}

		protected override void AddProcessOperation(MetalEquipmentManufacturingProcess process)
		{
			MetalData.Set(process);
		}

		protected override void RemoveProcessOperation(MetalEquipmentManufacturingProcess process)
		{
			int end = process.StartPosition + process.Length;
			for (byte i = process.StartPosition; i < end; i++)
				MetalData.Set(i);
		}

		protected override void RunProcess(Equipment equipment)
		{
			MetalData.Temperature = Data.Raw.MeltingPoint;
			foreach (MetalEquipmentManufacturingProcess process in Data.Processes.OrderBy(u => u.StartPosition).ToList())
			{
				process.Process(this,equipment);
				MetalData.Temperature -= MeltingPointFactor;
			}
		}

		private const int MeltingPointFactor = 512;

		private static byte GetForgeCellCount(IHasMeltingPoint row)
		{
			return (byte)(row.MeltingPoint / MeltingPointFactor + 1);
		}

		public override bool OnCreation(Role mainRole, Role manufacturingRole = null)
		{
			if (!base.OnCreation(mainRole, manufacturingRole)) return false;

			MetalData = new MetalEquipmentData(this);
			return true;
		}

		public override void OnShutdown()
		{
			base.OnShutdown();
			MetalData.Occupied = null;
			MetalData.Cells = null;
		}
	}
}