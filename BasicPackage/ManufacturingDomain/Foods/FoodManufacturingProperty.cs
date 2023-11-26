#region

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.Foods
{
	public class FoodManufacturingProperty : IRoleProperty, IArchivable,ILimitable
	{
		public List<FoodProcess> LearnedProcesses { get; } = new();

		public virtual void Save(IArchiveWriter writer)
		{
			writer.Write(LearnedProcesses.Count);
			foreach (FoodProcess process in LearnedProcesses)
			{
				writer.WriteObject(process);
			}
		}

		public virtual void Load(IArchiveReader reader)
		{
			int count = reader.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				LearnedProcesses.Add(reader.ReadObject<FoodProcess>());
			}
		}
		
		public bool CheckUseful()
		{
			return LearnedProcesses.Any();
		}

		public void Install(Role target)
		{
			
		}

		public void UnInstall(Role target)
		{
		}

		public void Overlay(IOverlayable<Role> otherOverlayable)
		{
		}

		public Type Id => typeof(FoodManufacturingProperty);
	}
}