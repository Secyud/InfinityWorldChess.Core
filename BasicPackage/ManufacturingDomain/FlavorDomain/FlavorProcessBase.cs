using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.FlavorDomain
{
	public abstract class FlavorProcessBase<TProcessData> :  IShowable, IHasContent
	{
		public virtual void SetContent(Transform transform)
		{
			transform.AddSimpleShown(this);
		}

		public abstract void Process(Manufacture manufacture,TProcessData processData);

		[field: S ]public string ShowName  { get; set; }

		[field: S ] public string ShowDescription { get; set; }

		[field: S ] public IObjectAccessor<Sprite> ShowIcon { get; set; }
        
		public abstract Color Color { get; }
	}
}