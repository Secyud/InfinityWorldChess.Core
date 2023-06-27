#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.Collections;
using Secyud.Ugf.TableComponents;
using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	[Registry]
	public abstract partial class EquipmentManufacturingContextBase
		<TRaw, TProcess, TBlueprint, TContext, TTableFunction, TProperty>
		where TContext : EquipmentManufacturingContextBase<TRaw, TProcess, TBlueprint, TContext, TTableFunction,
			TProperty>
		where TBlueprint : EquipmentManufacturingBlueprintBase<TRaw, TProcess, TBlueprint, TContext,
			TTableFunction,
			TProperty>
		where TRaw :  EquipmentManufacturable<TRaw, TProcess, TBlueprint, TContext, TTableFunction, TProperty>
		where TProcess : ManufacturingProcessBase<TContext,Equipment>
		where TTableFunction : TableFunctionBase<TProcess>
		where TProperty : ManufacturingPropertyBase<TProcess>
	{
		protected PrefabContainer<SButton> ManufacturingCell;
		public IMonoContainer<EquipmentManufacturingComponent> Factory;
		public RegistrableList<TProcess> DefaultProcesses { get; } = new();
		protected EquipmentManufacturingContextBase(IwcAb ab)
		{
			Factory = MonoContainer<EquipmentManufacturingComponent>.Create(ab);
			ManufacturingCell = PrefabContainer<SButton>.Create(
				ab,
				U.DotToPath(typeof(Manufacturable).Namespace) + "/" +
				nameof(ManufacturingCell) + ".prefab"
			);
		}

		public virtual bool OnCreation(Role mainRole, Role manufacturingRole = null)
		{
			if (mainRole is null)
			{
				Debug.LogWarning("Cannot open manufacturing while main role is null");
				return false;
			}

			Factory.Create();
			Data = new EquipmentData(mainRole, this as TContext);
			Factory.Value.OnInitialize(Data);

			List<TProcess> processes = null;
			TProperty property = manufacturingRole?.GetProperty<TProperty>();
			if (property != null)
				processes = property.LearnedProcesses;
			processes ??= DefaultProcesses.Get();

			Data.SetManufacturingRole(processes, Factory.Value.ProcessesTable);

			return true;
		}

		public virtual void OnShutdown()
		{
			Data = null;
			Factory.Destroy();
		}

		protected abstract void SelectBlueprint(TBlueprint blueprint);

		protected abstract void SelectRaw(TRaw raw);

		protected abstract bool ProcessAvailable(TProcess process);

		protected abstract void AddProcessOperation(TProcess process);

		protected abstract void RemoveProcessOperation(TProcess process);

		protected abstract void RunProcess(Equipment equipment);
	}
}