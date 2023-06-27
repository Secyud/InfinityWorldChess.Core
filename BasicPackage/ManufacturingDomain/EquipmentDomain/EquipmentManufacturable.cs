#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.TableComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
    public class EquipmentManufacturable<TRaw, TProcess, TBlueprint, TContext, TTableFunction, TProperty>
        : Manufacturable
        where TContext : EquipmentManufacturingContextBase<TRaw, TProcess, TBlueprint, TContext, TTableFunction,
            TProperty>
        where TBlueprint : EquipmentManufacturingBlueprintBase<TRaw, TProcess, TBlueprint, TContext,
            TTableFunction, TProperty>
        where TRaw : EquipmentManufacturable<TRaw, TProcess, TBlueprint, TContext, TTableFunction, TProperty>
        where TProcess : ManufacturingProcessBase<TContext, Equipment>
        where TTableFunction : TableFunctionBase<TProcess>
        where TProperty : ManufacturingPropertyBase<TProcess>
    {
        private int[] _property;

        public int[] Property => _property ??= new[]
        {
            Property00, Property01, Property10, Property11
        };

        [field: S(ID = 1)] public int Property00 { get; set; }
        [field: S(ID = 2)] public int Property01 { get; set; }
        [field: S(ID = 3)] public int Property10 { get; set; }
        [field: S(ID = 4)] public int Property11 { get; set; }
        [field: S(ID = 256)] public int Endurance { get; set; }

        public virtual void BeforeManufacturing(TContext context)
        {
        }

        public virtual void AfterManufacturing(TContext context)
        {
        }

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddShapeProperty(Property);
            transform.AddParagraph("应力:".Point() + Endurance);
        }
    }
}