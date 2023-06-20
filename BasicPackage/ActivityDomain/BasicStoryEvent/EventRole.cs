#region

using InfinityWorldChess.RoleDomain;

#endregion

namespace InfinityWorldChess.ActivityDomain.BasicStoryEvent
{
	public class EventRole
	{
		public Role Role { get; set; }

		public int Key { get; set; }

		public string Description { get; set; }
	}
}