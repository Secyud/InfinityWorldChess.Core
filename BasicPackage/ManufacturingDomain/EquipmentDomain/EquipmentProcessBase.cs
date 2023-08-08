#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.EquipmentDomain
{
    public abstract class EquipmentProcessBase : DataObject, IShowable, IHasContent
    {
        public virtual void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }

        public abstract void Process(Manufacture manufacture,EquipmentProcessData processData);

        public string ShowName => ObjectName;

        [field: S(ID = 1, DataType = DataType.Initialed)]
        public string ShowDescription { get; set; }

        [field: S(ID = 2, DataType = DataType.Initialed)]
        public IObjectAccessor<Sprite> ShowIcon { get; set; }
        
        public abstract Color Color { get; }
    }
}