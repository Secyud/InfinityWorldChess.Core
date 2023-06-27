using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.Collections;
using Secyud.Ugf.TableComponents;
using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	[Registry]
	public abstract partial class FlavorManufacturingContextBase
		<TFlavor,TComponent, TProcess, TContext, TProcessTf, TProperty>
		where TContext : FlavorManufacturingContextBase<TFlavor,TComponent, TProcess, TContext, TProcessTf, TProperty>
		where TProcess : FlavorManufacturingProcessBase<TContext,TFlavor>
		where TProcessTf : TableFunctionBase<TProcess>
		where TProperty : ManufacturingPropertyBase<TProcess>
		where TComponent : FlavorManufacturingComponent
		where TFlavor:class, IHasFlavor,IItem,IArchivableShown
	{
		public IMonoContainer<TComponent> Factory;
		public RegistrableList<TProcess> DefaultProcesses { get; } = new();

		public virtual bool OnCreation(Role mainRole, Role manufacturingRole = null)
		{
			if (mainRole is null)
			{
				Debug.LogWarning("Cannot open manufacturing while main role is null");
				return false;
			}

			Factory.Create();
			Data = new FlavorData(mainRole, this as TContext);
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
			foreach (TProcess process in Data.Processes)
				if (process.Flavor is IItem item)				
					Data.RoleItem.Add(item);

			Data = null;
			Factory.Destroy();
		}
		protected abstract void RunProcess(TFlavor drag );
		protected abstract TFlavor InitFlavor();
	}
}