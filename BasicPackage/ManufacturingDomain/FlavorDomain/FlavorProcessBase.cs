using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.FlavorDomain
{
	public abstract class FlavorProcessBase<TProcessData> : DataObject, IShowable, IHasContent
	{
		public virtual void SetContent(Transform transform)
		{
			transform.AddSimpleShown(this);
		}

		public abstract void Process(Manufacture manufacture,TProcessData processData);

		public string ShowName => ObjectName;

		[field: S(ID = 1, DataType = DataType.Initialed)]
		public string ShowDescription { get; set; }

		[field: S(ID = 2, DataType = DataType.Initialed)]
		public IObjectAccessor<Sprite> ShowIcon { get; set; }
        
		public abstract Color Color { get; }
	}
}