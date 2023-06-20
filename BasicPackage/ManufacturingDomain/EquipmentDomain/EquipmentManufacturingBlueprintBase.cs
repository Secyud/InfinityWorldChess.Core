#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using Secyud.Ugf.TableComponents;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class
		EquipmentManufacturingBlueprintBase<
			TRaw, TProcess, TBlueprint, TContext, TTableFunction, TProperty> :
			ManufacturingBlueprintBase<Equipment>
		where TContext : EquipmentManufacturingContextBase<
			TRaw, TProcess, TBlueprint, TContext, TTableFunction, TProperty>
		where TBlueprint : EquipmentManufacturingBlueprintBase<
			TRaw, TProcess, TBlueprint, TContext, TTableFunction, TProperty>
		where TRaw : EquipmentManufacturable<
			TRaw, TProcess, TBlueprint, TContext, TTableFunction, TProperty>
		where TTableFunction : TableFunctionBase<TProcess>
		where TProcess : ManufacturingProcessBase<TContext, Equipment>
		where TProperty : ManufacturingPropertyBase<TProcess>
	{
		public virtual Equipment Init(TContext context)
		{
			Equipment equipment = new();

			EquipmentManufacturingContextBase<TRaw, TProcess, TBlueprint, TContext, TTableFunction, TProperty>.
				EquipmentData data = context.Data;

			for (int i = 0; i < SharedConsts.EquipmentPropertyCount; i++)
				data.Property[i] = data.Raw.Property[i];

			data.Endurance = data.Raw.Endurance;

			return equipment;
		}

		public virtual void Finish(Equipment equipment, TContext context)
		{
			EquipmentManufacturingContextBase<TRaw, TProcess, TBlueprint, TContext, TTableFunction, TProperty>.
			EquipmentData data = context.Data;

			for (int i = 0; i < SharedConsts.EquipmentPropertyCount; i++)
				equipment.Property[i] = (int)data.Property[i];
			
			equipment.Antique = (int)data.Endurance;

			foreach (IParasiteBuff<Equipment> parasiteBuff in Buff)
			{
				equipment.Install(parasiteBuff, equipment);
			}
		}
	}
}