#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.EquipmentDomain
{
    public abstract class EquipmentProcessBase :  IShowable, IHasContent
    {
        public virtual void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }

        public abstract void Process(Manufacture manufacture,EquipmentProcessData processData);

        [field: S] public string ShowName  { get; set; }

        [field: S] public string ShowDescription { get; set; }

        [field: S] public IObjectAccessor<Sprite> ShowIcon { get; set; }
        
        public abstract Color Color { get; }
    }
}