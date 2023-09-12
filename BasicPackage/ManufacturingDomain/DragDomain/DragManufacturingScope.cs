using InfinityWorldChess.GameDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.ManufacturingDomain.DragDomain
{
	[Registry(DependScope = typeof(GameScope))]
	public class DragManufacturingScope : DependencyScopeProvider
	{
		private static MonoContainer<Manufacture> _manufactureContainer;

		public DragManufacturingScope(IwcAssets assets)
		{
			_manufactureContainer ??= MonoContainer<Manufacture>
				.Create(assets,"InfinityWorldChess/Manufacturing/Drag.prefab");
			_manufactureContainer.Create();
		}

		public override void Dispose()
		{
			_manufactureContainer.Destroy();
		}
	}
}