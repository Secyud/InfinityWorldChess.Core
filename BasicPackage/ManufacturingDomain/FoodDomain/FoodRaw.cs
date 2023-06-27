using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public class FoodRaw:FlavorRaw,IHasMouthfeel
	{
		[field:S(ID=261)]public float HardLevel { get; set; }

		[field:S(ID=262)]public float LimpLevel { get; set; }

		[field:S(ID=263)]public float WeakLevel { get; set; }

		[field:S(ID=264)]public float OilyLevel { get; set; }

		[field:S(ID=265)]public float SlipLevel { get; set; }

		[field:S(ID=266)]public float SoftLevel { get; set; }
		
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