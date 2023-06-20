using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Resource;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class FlavorRaw:Manufacturable,IHasFlavor
	{
		[R(256)]public float SpicyLevel { get; set; }

		[R(257)]public float SweetLevel { get; set; }

		[R(258)]public float SourLevel { get; set; }

		[R(259)]public float BitterLevel { get; set; }

		[R(260)]public float SaltyLevel { get; set; }
		
		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddFlavorInfo(this);
		}
	}
}