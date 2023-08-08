using InfinityWorldChess.GameDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.ManufacturingDomain.DragDomain
{
	[Registry(DependScope = typeof(GameScope))]
	public class DragManufacturingScope : DependencyScopeProvider
	{
		private static MonoContainer<Manufacture> _manufactureContainer;

		public DragManufacturingScope(IwcAb ab)
		{
			_manufactureContainer ??= MonoContainer<Manufacture>
				.Create(ab,"InfinityWorldChess/Manufacturing/Drag.prefab");
			_manufactureContainer.Create();
		}

		public override void Dispose()
		{
			_manufactureContainer.Destroy();
		}
	}
}