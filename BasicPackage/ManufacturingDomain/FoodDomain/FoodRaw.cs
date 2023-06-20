using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Resource;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public class FoodRaw:FlavorRaw,IHasMouthfeel
	{
		[R(261)]public float HardLevel { get; set; }

		[R(262)]public float LimpLevel { get; set; }

		[R(263)]public float WeakLevel { get; set; }

		[R(264)]public float OilyLevel { get; set; }

		[R(265)]public float SlipLevel { get; set; }

		[R(266)]public float SoftLevel { get; set; }
		
		public virtual void InitFood(FoodManufacturingContext context, Food food)
		{
		}

		public virtual void ProcessFood(FoodManufacturingContext context)
		{
		}

		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddMouthFeelInfo(this);
		}
	}
}