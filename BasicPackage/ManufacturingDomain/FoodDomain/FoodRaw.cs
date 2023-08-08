using System;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain.FlavorDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.FoodDomain
{
	public  class FoodRaw:Manufacturable,IHasMouthfeel
	{

		public float[] MouthFeelLevel { get; } = new float[BasicConsts.MouthFeelCount];
		
		public virtual void Init(FoodProcessData data)
		{
		}

		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddMouthFeelInfo(this);
		}
	}
}