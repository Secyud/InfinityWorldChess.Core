#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using System.Collections.Generic;
using InfinityWorldChess.BasicBundle.Items;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class ManufacturingBlueprintBase : ItemTemplate
	{
		
	}

	public abstract class ManufacturingBlueprintBase<TTarget> : ManufacturingBlueprintBase
		where TTarget : class, IParasitifer<TTarget>
	{
		public List<IParasiteBuff<TTarget>> Buff { get; } = new();

		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddListShown(U.T["特性"], Buff);
		}
	}
}