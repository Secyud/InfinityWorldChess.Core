using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class FlavorRaw:Manufacturable,IHasFlavor
	{
		[field:S(ID=256)]public float SpicyLevel { get; set; }

		[field:S(ID=257)]public float SweetLevel { get; set; }

		[field:S(ID=258)]public float SourLevel { get; set; }

		[field:S(ID=259)]public float BitterLevel { get; set; }

		[field:S(ID=260)]public float SaltyLevel { get; set; }
		
		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddFlavorInfo(this);
		}
	}
}