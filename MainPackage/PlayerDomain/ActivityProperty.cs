#region

using InfinityWorldChess.ActivityDomain;
using System.Collections.Generic;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
	public class ActivityProperty : List<ActivityGroup>,IArchivable
	{
		public void Save(IArchiveWriter writer)
		{
			writer.Write(Count);
			foreach (ActivityGroup activityGroup in this)
			{
				writer.WriteObject(activityGroup);
			}
		}

		public void Load(IArchiveReader reader)
		{
			int count = reader.ReadInt32();
			Clear();
			for (int i = 0; i < count; i++)
			{
				Add(reader.ReadObject<ActivityGroup>());
			}
		}

		public new void Add(ActivityGroup group)
		{
			if (group is not null)
			{
				base.Add(group);
				group.Install();
			}
		}
		public new void Remove(ActivityGroup group)
		{
			base.Remove(group);
			group?.UnInstall();
		}
	}
}