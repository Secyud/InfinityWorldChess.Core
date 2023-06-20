#region

using InfinityWorldChess.ItemDomain.FoodDomain;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class
		FoodManufacturingProcess : FlavorManufacturingProcessBase<FoodManufacturingContext,Food>
	{

		protected FoodManufacturingProcess(
			string name, string description, IObjectAccessor<Sprite> icon)
			: base(name, description, icon)
		{
		}
	}
}