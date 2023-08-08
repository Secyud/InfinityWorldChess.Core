#region

using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public class ItemGlobalService :IItemGlobalService
	{
		public List<IItem> List { get; } = new();
	}
}