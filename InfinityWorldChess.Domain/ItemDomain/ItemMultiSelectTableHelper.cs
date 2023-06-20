#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.TableComponents;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public class
		ItemMultiSelectTableHelper : 
			MultiSelectTableHelper<IItem, NormalCell, ItemTf, IwcTableHelperFh<IItem, ItemTf>>
	{
		public ItemMultiSelectTableHelper()
			: base(
				new IwcTableHelperFh<IItem, ItemTf>(),
				new IwcTableHelperFh<IItem, ItemTf>()
			)
		{
		}
	}
}