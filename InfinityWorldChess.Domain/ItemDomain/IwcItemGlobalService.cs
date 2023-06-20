#region

using Secyud.Ugf.DependencyInjection;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public class IwcItemGlobalService :IItemGlobalService, ISingleton
	{
		public List<IItem> List { get; } = new();
	}
}