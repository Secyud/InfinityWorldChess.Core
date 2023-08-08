#region

using InfinityWorldChess.GameDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.StoneDomain
{
	[Registry(DependScope = typeof(GameScope))]
	public class StoneEquipmentManufacturingScope:DependencyScopeProvider
	{
		private static MonoContainer<Manufacture> _manufactureContainer;

		public StoneEquipmentManufacturingScope(IwcAb ab)
		{
			_manufactureContainer ??= MonoContainer<Manufacture>
				.Create(ab,"InfinityWorldChess/Manufacturing/StoneEquipment.prefab");
			_manufactureContainer.Create();
		}

		public override void Dispose()
		{
			_manufactureContainer.Destroy();
		}
	}
}