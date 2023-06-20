#region

using Secyud.Ugf;
using Secyud.Ugf.TableComponents;
using System;

#endregion

namespace InfinityWorldChess.Ugf
{
	public class IwcTableHelperF<TItem, TListService> : FunctionalTableHelper<TItem, NormalCell, TListService>
		where TItem : ICanBeShown where TListService : TableFunctionBase<TItem>
	{
		public event Action<NormalCell, TItem> SetCellAction; 
		protected override void SetCell(NormalCell cell, TItem item)
		{
			SetCellAction?.Invoke(cell,item);
			cell.OnInitialize(item);
		}
	}
}