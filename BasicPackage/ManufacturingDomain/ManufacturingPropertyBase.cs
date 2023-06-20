#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Archiving;
using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class ManufacturingPropertyBase<TProcess> : RoleProperty, IArchivable
		where TProcess : class
	{
		public readonly List<TProcess> LearnedProcesses = new();


		public virtual void Save(BinaryWriter writer)
		{
			writer.Write(LearnedProcesses.Count);
			foreach (TProcess process in LearnedProcesses)
				writer.WriteArchiving(process);
		}

		public virtual void Load(BinaryReader reader)
		{
			int count = reader.ReadInt32();
			for (int i = 0; i < count; i++)
				LearnedProcesses.Add(reader.ReadArchiving<TProcess>());
		}
	}
}