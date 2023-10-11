#region

using InfinityWorldChess.ActivityDomain;
using System.Collections.Generic;
using System.Linq;
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

		public void AddReceivedActivity(ActivityGroup group,int startIndex = 0)
		{
			group.State = ActivityState.Received;
			IActivity activity = group.Activities[startIndex];
			group.CurrentActivity = activity;
			activity.State = ActivityState.Received;
			activity.StartActivity(group);
		}
	}
}