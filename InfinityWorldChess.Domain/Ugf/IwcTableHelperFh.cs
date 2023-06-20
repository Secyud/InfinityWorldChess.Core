#region

using Secyud.Ugf;
using Secyud.Ugf.TableComponents;

#endregion

namespace InfinityWorldChess.Ugf
{
	public class IwcTableHelperFh<TItem, TListService> : IwcTableHelperF<TItem, TListService>
		where TItem : ICanBeShown, IHasContent where TListService : TableFunctionBase<TItem>
	{
		protected override void SetCell(NormalCell cell, TItem item)
		{
			base.SetCell(cell, item);
			cell.SetFloating(item);
		}
	}
}