using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.ItemDomain
{
	public interface IItemGlobalService:IRegistry
	{
		List<IItem> List { get; }
	}
}