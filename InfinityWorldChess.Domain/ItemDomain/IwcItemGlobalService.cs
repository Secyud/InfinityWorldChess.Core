#region

using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	[Registry()]
	public class IwcItemGlobalService :IItemGlobalService
	{
		public List<IItem> List { get; } = new();
	}
}