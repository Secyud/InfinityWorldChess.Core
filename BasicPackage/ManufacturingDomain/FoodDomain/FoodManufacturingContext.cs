using InfinityWorldChess.GameDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.ManufacturingDomain.FoodDomain
{
	[Registry(DependScope = typeof(GameScope))]
	public class FoodManufacturingScope : DependencyScopeProvider
	{
		private static MonoContainer<Manufacture> _manufactureContainer;

		public FoodManufacturingScope(IwcAb ab)
		{
			_manufactureContainer ??= MonoContainer<Manufacture>
				.Create(ab,"InfinityWorldChess/Manufacturing/Food.prefab");
			_manufactureContainer.Create();
		}

		public override void Dispose()
		{
			_manufactureContainer.Destroy();
		}
		
	}
}