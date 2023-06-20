#region

using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.ActivityDomain.BasicStoryEvent
{
	public class EventNode
	{
		public List<EventSelectNode> NextNodes = new();
		public List<EventRole> Roles = new();
	}
}