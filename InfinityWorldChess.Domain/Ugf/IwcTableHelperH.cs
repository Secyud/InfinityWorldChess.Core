#region

using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.Ugf
{
	public class IwcTableHelperH<TItem> : IwcTableHelper<TItem>
		where TItem : ICanBeShown, IHasContent
	{
		protected override void SetCell(NormalCell cell, TItem item)
		{
			base.SetCell(cell, item);
			cell.SetFloating(item);
		}
	}
}