using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using System;

namespace InfinityWorldChess.ManufacturingDomain
{
	public class DragManufacturingContext : FlavorManufacturingContextBase
	<Drag, DragManufacturingComponent, DragManufacturingProcess, DragManufacturingContext,
		DragManufacturingProcessTf, DragManufacturingProperty>
	{

		public DragManufacturingData DragData { get; private set; }

		public class DragManufacturingData : IHasFlavor
		{
			public float SpicyLevel { get; set; }

			public float SweetLevel { get; set; }

			public float SourLevel { get; set; }

			public float BitterLevel { get; set; }

			public float SaltyLevel { get; set; }
		}

		private struct Flavor : IHasFlavor
		{

			public float SpicyLevel { get; set; }

			public float SweetLevel { get; set; }

			public float SourLevel { get; set; }

			public float BitterLevel { get; set; }

			public float SaltyLevel { get; set; }
		}

		protected override void RunProcess(Drag drag)
		{
			foreach (DragManufacturingProcess process in Data.Processes)
			{
				if (process.Flavor is DragRaw dragRaw)
					dragRaw.ProcessDrag(this, drag);

				if (process.Flavor is not null)
				{
					Flavor flavor = new();
					for (int i = 0; i < 5; i++)
					{
						float add = process.Flavor.GetFlavor(i) * 0.1f;
						float ori = drag.GetFlavor(i) * 0.1f;
						float absAdd = Math.Abs(add);
						float absOri = Math.Abs(ori);

						if (add * ori < 0)
						{
							if (absAdd < absOri)
								flavor.SetFlavor(i - 2, add + ori);
							else
								flavor.SetFlavor(i + 2, add + ori);
						}
						else
						{
							if (absAdd < absOri)
								flavor.SetFlavor(i - 1, add - ori);
							else
								flavor.SetFlavor(i + 1, add - ori);
						}
					}
					flavor.CopyFlavorTo(drag);
				}

				process.Process(this, drag);
			}
		}

		protected override Drag InitFlavor()
		{
			return new Drag();
		}

		public override bool OnCreation(Role mainRole, Role manufacturingRole = null)
		{
			if (base.OnCreation(mainRole, manufacturingRole))
			{
				DragData = new DragManufacturingData();
				return true;
			}
			return false;
		}


		public DragManufacturingContext(IwcAb ab) : base(ab)
		{
		}
	}
}