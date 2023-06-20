using InfinityWorldChess.ItemDomain.FoodDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.ManufacturingDomain
{
	public interface IFlavorProcess:IHasContent,ICanBeShown
	{
		public IHasFlavor Flavor { get; set; }
	}
}