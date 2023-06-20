using InfinityWorldChess.ItemDomain.FoodDomain;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class FlavorManufacturingProcessBase<TContext,TFlavor> : 
		ManufacturingProcessBase<TContext,TFlavor>,IFlavorProcess
	{
		public int TimeAdd { get; set; }

		public IHasFlavor Flavor { get; set; }

		protected FlavorManufacturingProcessBase(string name, string description, IObjectAccessor<Sprite> icon) :
			base(name, description, icon)
		{
		}
	}
}