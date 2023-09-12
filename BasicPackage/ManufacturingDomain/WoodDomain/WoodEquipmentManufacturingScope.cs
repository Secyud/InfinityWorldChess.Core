#region

using InfinityWorldChess.GameDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.WoodDomain
{
	[Registry(DependScope = typeof(GameScope))]
	public class WoodEquipmentManufacturingScope :DependencyScopeProvider
	{
		private static MonoContainer<Manufacture> _manufactureContainer;

		public WoodEquipmentManufacturingScope(IwcAssets assets)
		{
			_manufactureContainer ??= MonoContainer<Manufacture>
				.Create(assets,"InfinityWorldChess/Manufacturing/WoodEquipment.prefab");
			_manufactureContainer.Create();
		}

		public override void Dispose()
		{
			_manufactureContainer.Destroy();
		}
	}
}