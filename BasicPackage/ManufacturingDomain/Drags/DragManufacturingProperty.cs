#region

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.Drags
{
	public class DragManufacturingProperty : IRoleProperty, IArchivable,ILimitable
	{
		public List<DragBlueprint> LearnedBlueprints { get; } = new();

		public virtual void Save(IArchiveWriter writer)
		{
			writer.Write(LearnedBlueprints.Count);
			foreach (DragBlueprint process in LearnedBlueprints)
			{
				writer.WriteObject(process);
			}
		}

		public virtual void Load(IArchiveReader reader)
		{
			int count = reader.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				LearnedBlueprints.Add(reader.ReadObject<DragBlueprint>());
			}
		}
		
		public bool CheckUseful()
		{
			return LearnedBlueprints.Any();
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

		public Type Id => typeof(DragManufacturingProperty);
	}
}