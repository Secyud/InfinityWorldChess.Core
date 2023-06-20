#region

using System;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace InfinityWorldChess.ActivityDomain.BasicStoryEvent
{
	public class EventSelectNode
	{
		[NotNull] public EventSelectNode NextNode;
		[NotNull] public EventSelectNode PreNode;

		[NotNull] public Func<EventSelectNode, bool> SelectCondition;

		public bool Selectable => SelectCondition(this);
	}
}