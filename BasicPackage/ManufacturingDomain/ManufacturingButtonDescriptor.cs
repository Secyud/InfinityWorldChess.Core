#region

using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ManufacturingDomain.DragDomain;
using InfinityWorldChess.ManufacturingDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain.MetalDomain;
using InfinityWorldChess.ManufacturingDomain.StoneDomain;
using InfinityWorldChess.ManufacturingDomain.WoodDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public sealed class ManufacturingButtonDescriptor : ButtonDescriptor<WorldCell>
	{
		private static readonly string[] Names = {"铁匠铺", "木匠铺", "石匠铺", "餐馆", "药铺"};

		public int Type { get; set; }

		public override string ShowName => Names[Type];

		public override bool Visible(WorldCell target) => true;

		public override void Trigger()
		{
			IDependencyManager dm = U.M;
			switch (Type)
			{
			case 0:
			{
				dm.CreateScope<MetalEquipmentManufacturingScope>();
				break;
			}
			case 1:
			{
				dm.CreateScope<WoodEquipmentManufacturingScope>();
				break;
			}
			case 2:
			{
				dm.CreateScope<StoneEquipmentManufacturingScope>();
				break;
			}
			case 3:
			{
				dm.CreateScope<FoodManufacturingScope>();
				break;
			}
			case 4:
			{
				dm.CreateScope<DragManufacturingScope>();
				break;
			}
			}
		}

		public void Save(IArchiveWriter writer)
		{
			writer.Write(Type);
		}

		public void Load(IArchiveReader reader)
		{
			Type = reader.ReadInt32();
		}
	}
}