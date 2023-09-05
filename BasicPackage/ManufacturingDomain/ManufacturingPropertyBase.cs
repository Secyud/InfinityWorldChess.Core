﻿#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Archiving;
using System.Collections.Generic;
using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class ManufacturingPropertyBase<TProcess> : RoleProperty, IArchivable
		where TProcess : class
	{
		public readonly List<TProcess> LearnedProcesses = new();

		public virtual void Save(IArchiveWriter writer)
		{
			writer.Write(LearnedProcesses.Count);
			foreach (TProcess process in LearnedProcesses)
				writer.WriteObject(process);
		}

		public virtual void Load(IArchiveReader reader)
		{
			int count = reader.ReadInt32();
			for (int i = 0; i < count; i++)
				LearnedProcesses.Add(reader.ReadObject<TProcess>());
		}
	}
}