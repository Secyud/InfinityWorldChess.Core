#region

using InfinityWorldChess.ItemDomain.FoodDomain;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class
		DragManufacturingProcess : FlavorManufacturingProcessBase
			<DragManufacturingContext,Drag>
	{

		protected DragManufacturingProcess(
			string name, string description, IObjectAccessor<Sprite> icon)
			: base(name, description, icon)
		{
		}
	}
}