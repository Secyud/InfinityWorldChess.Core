#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.EquipmentDomain
{
    public class EquipmentManufacturable : Manufacturable
    {
        [S ] public readonly int[] Property = new int[SharedConsts.EquipmentPropertyCount];
        [S ] public readonly int[] Shape = new int[SharedConsts.EquipmentPropertyCount];
        
        public virtual void BeforeManufacturing(Manufacture manufacture,EquipmentProcessData processData)
        {
        }

        public virtual void AfterManufacturing(Manufacture manufacture,EquipmentProcessData processData)
        {
        }

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddShapeProperty(Property,"属性");
            transform.AddShapeProperty(Shape);
        }
    }
}