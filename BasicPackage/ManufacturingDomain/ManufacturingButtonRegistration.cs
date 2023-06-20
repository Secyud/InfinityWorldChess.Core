#region

using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf;
using Secyud.Ugf.ButtonComponents;
using System.IO;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public sealed class ManufacturingButtonRegistration : ButtonRegistration<WorldChecker>
	{
		private static readonly string[] Names = {"铁匠铺", "木匠铺", "石匠铺", "餐馆", "药铺"};

		public int Type { get; set; }

		public override string ShowName => Names[Type];

		public override bool Visible(WorldChecker target) => true;

		public override void Trigger()
		{
			Role role = GameScope.PlayerGameContext.Role;
			GameScope scope = Og.ScopeFactory.GetScope<GameScope>();
			switch (Type)
			{
			case 0:
			{
				MetalEquipmentManufacturingContext context = scope.Get<MetalEquipmentManufacturingContext>();
				context.OnCreation(role);
				break;
			}
			case 1:
			{
				WoodEquipmentManufacturingContext context = scope.Get<WoodEquipmentManufacturingContext>();
				context.OnCreation(role);
				break;
			}
			case 2:
			{
				StoneEquipmentManufacturingContext context = scope.Get<StoneEquipmentManufacturingContext>();
				context.OnCreation(role);
				break;
			}
			case 3:
			{
				FoodManufacturingContext context = scope.Get<FoodManufacturingContext>();
				context.OnCreation(role);
				break;
			}
			case 4:
			{
				DragManufacturingContext context = scope.Get<DragManufacturingContext>();
				context.OnCreation(role);
				break;
			}
			}
		}

		public void Save(BinaryWriter writer)
		{
			writer.Write(Type);
		}

		public void Load(BinaryReader reader)
		{
			Type = reader.ReadInt32();
		}
	}
}