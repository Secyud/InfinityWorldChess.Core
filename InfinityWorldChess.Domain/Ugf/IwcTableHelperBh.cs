#region

using Secyud.Ugf;
using Secyud.Ugf.ButtonComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine.Events;

#endregion

namespace InfinityWorldChess.Ugf
{
	public class IwcTableHelperBh<TItem, TListService, TButtonService> :
		IwcTableHelperB<TItem, TListService, TButtonService>
		where TItem : ICanBeShown, IHasContent
		where TListService : TableFunctionBase<TItem>
		where TButtonService : ButtonFunctionBase<TItem>
	{
		protected override void SetCell(NormalCell cell, TItem item)
		{
			base.SetCell(cell, item);
			cell.SetFloating(item);
		}

		public IwcTableHelperBh(UnityAction refreshAction) : base(refreshAction)
		{
		}
	}
}