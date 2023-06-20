#region

using Secyud.Ugf;
using Secyud.Ugf.TableComponents;

#endregion

namespace InfinityWorldChess.Ugf
{
	public class IwcTableHelperS<TItem, TListService> : SelectableTableHelper<TItem, NormalCell, TListService>
		where TItem : ICanBeShown
		where TListService : TableFunctionBase<TItem>
	{
		protected override void SetCell(NormalCell cell, TItem item)
		{
			cell.OnInitialize(item);
		}
	}
}