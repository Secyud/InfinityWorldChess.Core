#region

using InfinityWorldChess.GameDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.MetalDomain
{
	[Registry(DependScope = typeof(GameScope))]
	public class MetalEquipmentManufacturingScope:DependencyScopeProvider
	{
		private static MonoContainer<Manufacture> _manufactureContainer;

		public MetalEquipmentManufacturingScope(IwcAssets assets)
		{
			_manufactureContainer ??= MonoContainer<Manufacture>
				.Create(assets,"InfinityWorldChess/Manufacturing/MetalEquipment.prefab");
			_manufactureContainer.Create();
		}

		public override void Dispose()
		{
			_manufactureContainer.Destroy();
		}
	}
}