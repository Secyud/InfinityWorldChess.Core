#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.Layout;
using System.Linq;
using InfinityWorldChess.Ugf;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public class WoodEquipmentManufacturingContext :
		EquipmentManufacturingContextBase<
			WoodEquipmentRaw, WoodEquipmentManufacturingProcess, WoodEquipmentManufacturingBlueprint,
			WoodEquipmentManufacturingContext, WoodEquipmentManufacturingProcessTf,
			WoodEquipmentManufacturingProperty>
	{
		public WoodEquipmentData WoodData { get; private set; }

		public class WoodEquipmentData
		{
			public bool[,] Occupied { get; private set; }

			public SButton[,] Cells { get; private set; }

			private readonly WoodEquipmentManufacturingContext _context;
			private readonly EquipmentData _data;
			private readonly LayoutGroupTrigger _layout;
			private readonly GridLayoutGroup _gridLayout;

			public WoodEquipmentData(WoodEquipmentManufacturingContext context)
			{
				_context = context;
				_data = context.Data;
				_layout = _context.Factory.Value.CellContent;
				_gridLayout = _layout.Element as GridLayoutGroup;
				if (_gridLayout != null)
					_gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			}

			public void Set(byte x, byte y)
			{
				Occupied[x, y] = false;
				SButton cell = Cells[x, y];
				cell.GetComponent<SImage>().color = Color.white;
				cell.Bind(
					() =>
					{
						WoodEquipmentManufacturingProcess process = _data.SelectedProcess;
						process.PositionX = x;
						process.PositionY = y;
						_data.AddProcess(process);
					}
				);
			}

			public void Set(WoodEquipmentManufacturingProcess process)
			{
				foreach (PairStruct<byte, byte> r in process.Range)
				{
					int xt = process.PositionX + r.First;
					int yt = process.PositionY + r.Second;
					Occupied[xt, yt] = true;
					SButton cell = Cells[xt, yt];
					cell.GetComponent<SImage>().color = process.Color;
					cell.Bind(() => _data.RemoveProcess(process));
				}
			}

			public void Create(int width, int height)
			{
				RectTransform content = _layout.PrepareLayout();
				_gridLayout.constraintCount = width;
				Occupied = new bool[width, height];
				Cells = new SButton[width, height];
				for (byte i = 0; i < width; i++)
				for (byte j = 0; j < height; j++)
				{
					Cells[i, j] = _context.ManufacturingCell.Instantiate(content);
					Set(i, j);
				}
			}

			public void Delete(WoodEquipmentManufacturingContext context)
			{
				context.Factory.Value.CellContent.PrepareLayout();
				Occupied = null;
				Cells = null;
			}
		}

		public WoodEquipmentManufacturingContext(IwcAb ab) : base(ab)
		{
		}

		protected override void SelectBlueprint(WoodEquipmentManufacturingBlueprint blueprint)
		{
			
		}

		protected override void SelectRaw(WoodEquipmentRaw woodEquipmentRaw)
		{
			if (woodEquipmentRaw is null)
				WoodData.Delete(this);
			else
				WoodData.Create(
					woodEquipmentRaw.Width,
					woodEquipmentRaw.Height
				);
		}

		protected override bool ProcessAvailable(WoodEquipmentManufacturingProcess process)
		{
			return !(from r in process.Range
				let xt = process.PositionX + r.First
				let yt = process.PositionY + r.Second
				where xt >= Data.Raw.Width ||
					yt >= Data.Raw.Height || 
					WoodData.Occupied[xt, xt]
				select xt).Any();
		}

		protected override void AddProcessOperation(WoodEquipmentManufacturingProcess process)
		{
			WoodData.Set(process);
		}

		protected override void RemoveProcessOperation(WoodEquipmentManufacturingProcess process)
		{
			foreach (PairStruct<byte, byte> r in process.Range)
			{
				byte xt = (byte)(process.PositionX + r.First);
				byte yt = (byte)(process.PositionY + r.Second);
				WoodData.Set(xt, yt);
			}
		}

		protected override void RunProcess(Equipment equipment)
		{
			foreach (WoodEquipmentManufacturingProcess process in Data.Processes
				.OrderBy(u =>
						u.PositionY * Data.Raw.Width + u.PositionX
				).ToList())
				process.Process(this,equipment);
		}

		public override bool OnCreation(Role mainRole, Role manufacturingRole = null)
		{
			if (!base.OnCreation(mainRole, manufacturingRole)) return false;

			WoodData = new WoodEquipmentData(this);
			return true;
		}

		public override void OnShutdown()
		{
			base.OnShutdown();
			WoodData = null;
		}
	}
}