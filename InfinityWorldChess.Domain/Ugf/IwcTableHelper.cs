#region

using Secyud.Ugf;
using Secyud.Ugf.TableComponents;
using System;

#endregion

namespace InfinityWorldChess.Ugf
{
	public class IwcTableHelper<TItem> : TableHelper<TItem, NormalCell>
		where TItem : ICanBeShown
	{
		public event Action<NormalCell, TItem> SetCellAction; 

		protected override void SetCell(NormalCell cell, TItem item)
		{
			SetCellAction?.Invoke(cell,item);
			cell.OnInitialize(item);
		}
	}
}