#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Resource;
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
		public int[] Property { get; } = new int[SharedConsts.EquipmentPropertyCount];

		[R(256)] public int Endurance { get; set; }

		public virtual void BeforeManufacturing(TContext context)
		{
		}

		public virtual void AfterManufacturing(TContext context)
		{
		}

		protected override void SetDefaultValue( )
		{
			base.SetDefaultValue();
			for (byte i = 0; i < SharedConsts.EquipmentPropertyCount; i++)
			{
				Property[i] = Descriptor.Get<short>(512 + i);
			}
		}

		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddShapeProperty(Property);
			transform.AddParagraph("应力:".Point() + Endurance);
		}
	}
}