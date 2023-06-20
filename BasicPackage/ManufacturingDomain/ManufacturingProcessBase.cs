#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class ManufacturingProcessBase<TContext,TTarget> : ICanBeShown, IHasContent
	{
		protected ManufacturingProcessBase(string name, string description, IObjectAccessor<Sprite> icon)
		{
			ShowName = name;
			ShowDescription = description;
			ShowIcon = icon;
		}

		public virtual void SetContent(Transform transform)
		{
			transform.AddSimpleShown(this);
		}

		public abstract void Process(TContext contextBase,TTarget target);

		public string ShowName { get; }

		public string ShowDescription { get; }

		public IObjectAccessor<Sprite> ShowIcon { get; }

		public abstract Color Color { get; }
	}
}