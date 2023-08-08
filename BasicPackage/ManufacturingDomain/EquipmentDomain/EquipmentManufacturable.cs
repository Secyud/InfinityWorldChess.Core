#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.TableComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.EquipmentDomain
{
    public class EquipmentManufacturable : Manufacturable
    {
        [S(ID = 1)] public readonly int[] Property = new int[SharedConsts.EquipmentPropertyCount];
        [S(ID = 2)] public readonly int[] Shape = new int[SharedConsts.EquipmentPropertyCount];
        
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