using System.Collections.Generic;

namespace InfinityWorldChess.ItemDomain
{
	public interface IItemGlobalService
	{
		List<IItem> List { get; }
	}
}